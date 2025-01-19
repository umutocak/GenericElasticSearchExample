using GenericElasticSearchExample.Business.Abstract;
using GenericElasticSearchExample.Business.ElasticSearch.Abstract;
using GenericElasticSearchExample.Business.Utilities.Consts;
using GenericElasticSearchExample.Entities.DTOs.ElasticSearch;
using GenericElasticSearchExample.Entities.DTOs.Products;

namespace GenericElasticSearchExample.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IElasticsearchService _elasticsearchService;

        public ProductManager(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.GetDocumentsAsync<Product>(ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<Product> GetById(string id, CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.GetDocumentAsync<Product>(id, ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> GetProductDetails(CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.SearchAsync<Product>(p => p.Index(ElasticsearchIndexes.Products).From(0).Size(10), cancellationToken);
            return data;
        }

        public async Task<object> Match(string queryKeyword, CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.MatchQueryAsync<Product>(p => p.Name, queryKeyword, ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> Fuzzy(string queryKeyword, CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.FuzzyQueryAsync<Product>(p => p.Name, queryKeyword, ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> Wildcard(string queryKeyword, CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.WildcardQueryAsync<Product>(p => p.Name, queryKeyword, ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> Exists(CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.ExistsQueryAsync<Product>(p => p.Name, ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> Bool(CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.BoolQueryAsync<Product>(p => p.Name,"notebook",p => p.Name,"bilgisayar",p => p.Name,"*s*", ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> Term(string queryKeyword, CancellationToken cancellationToken)
        {
            var data = await _elasticsearchService.TermQueryAsync<Product>(p => p.Name, queryKeyword, ElasticsearchIndexes.Products, cancellationToken);
            return data;
        }

        public async Task<object> Count(CancellationToken cancellationToken)
        {
            var count = await _elasticsearchService.CountDocumentsAsync<Product>(ElasticsearchIndexes.Products, cancellationToken);
            return count;
        }

        public async Task<object> Create(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Quantity = createProductDto.Quantity
            };
            bool result = await _elasticsearchService.CreateDocumentAsync(product, ElasticsearchIndexes.Products, cancellationToken);
            return result;
        }

        public async Task<object> Update(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            bool result = await _elasticsearchService.UpdateDocumentAsync<Product>(updateProductDto.Id, updateProductDto, ElasticsearchIndexes.Products, cancellationToken);
            return result;
        }

        public async Task<object> Delete(string id, CancellationToken cancellationToken)
        {
            bool result = await _elasticsearchService.DeleteDocumentAsync<Product>(id, ElasticsearchIndexes.Products, cancellationToken);
            return result;
        }
    }
}
