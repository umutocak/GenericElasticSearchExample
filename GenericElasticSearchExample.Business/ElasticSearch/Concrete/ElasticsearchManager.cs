using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.TermVectors;
using Elastic.Clients.Elasticsearch.Nodes;
using GenericElasticSearchExample.Business.ElasticSearch.Abstract;
using GenericElasticSearchExample.Business.Utilities.Consts;
using GenericElasticSearchExample.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericElasticSearchExample.Business.ElasticSearch.Concrete
{
    public class ElasticsearchManager : IElasticsearchService
    {
        readonly ElasticsearchClient _elasticsearchClient;

        public ElasticsearchManager(IConfiguration configuration)
        {
            ElasticsearchClientSettings elasticsearchClientSettings = new(new Uri(configuration["ElasticsearchSettings:Uri"]));
            elasticsearchClientSettings.DefaultIndex(ElasticsearchIndexes.DefaultIndex);
            _elasticsearchClient = new(elasticsearchClientSettings);
        }

        //Bool sorgusu, birden fazla sorguyu bir araya getirip mantıksal ilişki ile birleştirmemizi sağlamakta ve böylece karmaşık sorgu yapıları oluşturmamıza olanak tanımaktadır.
        public async Task<IReadOnlyCollection<T>> BoolQueryAsync<T>(Expression<Func<T, object>> matchField, string matchQueryKeyword, Expression<Func<T, object>> fuzzyField, string fuzzyQueryKeyword, Expression<Func<T, object>> wildcardField, string wildcardQueryKeyword, string indexName = "products", CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index.Index(indexName)
            .Query(query => query.Bool(t => t.Should(
                match => match.Match(t => t.Field(matchField).Query(matchQueryKeyword)),
                fuzzy => fuzzy.Fuzzy(p => p.Field(fuzzyField).Value(fuzzyQueryKeyword)),
                wildcard => wildcard.Wildcard(w => w.Field(wildcardField).Value(wildcardQueryKeyword))
                )))
            .From(from)
            .Size(size), cancellationToken);
            return searchResponse.Documents;
        }

        public async Task<long> CountDocumentsAsync<T>(string indexName = "products", CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            CountResponse countResponse = await _elasticsearchClient.CountAsync<T>(indexName, cancellationToken);
            return countResponse.Count;
        }

        public async Task<bool> CreateDocumentAsync<T>(T document, string indexName = "products", CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            CreateRequest<T> createRequest = new(document, indexName, document.Id);
            CreateResponse createResponse = await _elasticsearchClient.CreateAsync(createRequest, cancellationToken);
            return createResponse.IsSuccess();
        }

        public async Task<bool> CreateIndexAsync(string indexName = "products", CancellationToken cancellationToken = default)
        {
            IndexResponse indexResponse = await _elasticsearchClient.IndexAsync(indexName, cancellationToken);
            return indexResponse.IsSuccess();
        }

        public async Task<bool> DeleteDocumentAsync<T>(string documentId, string indexName = "products", CancellationToken cancellationToken = default)
        {
            DeleteResponse deleteResponse = await _elasticsearchClient.DeleteAsync<T>(indexName, documentId, cancellationToken);
            return deleteResponse.IsSuccess();
        }

        public async Task<IReadOnlyCollection<T>> ExistsQueryAsync<T>(Expression<Func<T, object>> field, string indexName = "products", CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index.Index(indexName)
            .Query(query => query.Exists(t => t.Field(field))).From(from).Size(size));
            return searchResponse.Documents;
        }

        /*Özellikle arama süreçlerinde kullanıcılar tarafından yanlış yazımdan kaynaklı kelime hatalarının söz konusu olduğu aramalarda benzer kelimeleri bulmak için
        fuzzy aramasından istifade edebiliriz.*/
        public async Task<IReadOnlyCollection<T>> FuzzyQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = "products", CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index.Index(indexName)
            .Query(query => query.
            Fuzzy(
                t => t.Field(field)
                .Value(queryKeyword)))
            .From(from)
            .Size(size), cancellationToken);
            return searchResponse.Documents;
        }

        public async Task<T> GetDocumentAsync<T>(string documentId, string indexName = "products", CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            GetResponse<T> getResponse = await _elasticsearchClient.GetAsync<T>(documentId, index => index.Index(indexName), cancellationToken);
            return getResponse.Source;
        }

        public async Task<IReadOnlyCollection<T>> GetDocumentsAsync<T>(string indexName = "products", CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(indexName, cancellationToken);
            return searchResponse.Documents;
        }

        //Arama sürecinde bir alandaki metni analiz eder, analiz sonucunda elde edilen terimleri kullanarak aramayı gerçekleştirir.
        public async Task<IReadOnlyCollection<T>> MatchQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = "products", CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index.Index(indexName).
            Query(query => query.
            Match(
                t => t.Field(field).
                Query(queryKeyword))).
                From(from).
                Size(size), cancellationToken);

            return searchResponse.Documents;
        }

        public async Task<IReadOnlyCollection<T>> SearchAsync<T>(Action<SearchRequestDescriptor<T>> searchRequestDescriptor, CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(searchRequestDescriptor, cancellationToken);
            return searchResponse.Documents;
        }

        //Eğer ki tam olarak belirtilen ifadeyi eşleştirmek istiyorsak term aramasını kullanabiliriz
        public async Task<IReadOnlyCollection<T>> TermQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = "products", CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index.Index(indexName)
            .Query(query => query.Term( t=> t.Field(field).Value(queryKeyword)))
            .From(from)
            .Size(size));
            return searchResponse.Documents;
        }

        // Dökümanları güncellemek içindir
        public async Task<bool> UpdateDocumentAsync<T>(string documentId, object partialDocument, string indexName = "products", CancellationToken cancellationToken = default)
        {
            UpdateRequest<T, object> updateRequest = new(indexName, documentId)
            {
                Doc = partialDocument
            };

            UpdateResponse<T> updateResponse = await _elasticsearchClient.UpdateAsync<T, object>(updateRequest, cancellationToken);
            return updateResponse.IsSuccess();
        }

        /*Veri sorgulama sürecinde belirli bir kalıba uygun taramada bulunabilmek için yıldız(*) ve soru işareti(?) gibi joker karakterlerden istifade etmekteyiz.
         * *test* => Örneğin, verilen alan içerisinde test ifadesini arar ve eşleşini getirir.
         * ?s* => ikinci harfi 's' ile başlayanları getirir
         */
        public async Task<IReadOnlyCollection<T>> WildcardQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = "products", CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(
                index => index.Index(indexName).
            Query(query => query.
            Wildcard(t => t.
            Field(field).Value(queryKeyword)))
            .From(from)
            .Size(size), cancellationToken);
            return searchResponse.Documents;
        }
    }
}
