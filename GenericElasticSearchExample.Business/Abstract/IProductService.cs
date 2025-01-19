
using GenericElasticSearchExample.Entities.DTOs.ElasticSearch;
using GenericElasticSearchExample.Entities.DTOs.Products;

namespace GenericElasticSearchExample.Business.Abstract
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<Product> GetById(string id, CancellationToken cancellationToken);
        Task<object> GetProductDetails(CancellationToken cancellationToken);
        Task<object> Match(string queryKeyword, CancellationToken cancellationToken);
        Task<object> Fuzzy(string queryKeyword, CancellationToken cancellationToken);
        Task<object> Wildcard(string queryKeyword, CancellationToken cancellationToken);
        Task<object> Exists(CancellationToken cancellationToken);
        Task<object> Bool(CancellationToken cancellationToken);
        Task<object> Term(string queryKeyword, CancellationToken cancellationToken);
        Task<object> Count(CancellationToken cancellationToken);
        Task<object> Create(CreateProductDto createProductDto, CancellationToken cancellationToken);
        Task<object> Update(UpdateProductDto updateProductDto, CancellationToken cancellationToken);
        Task<object> Delete(string id, CancellationToken cancellationToken);
    }
}
