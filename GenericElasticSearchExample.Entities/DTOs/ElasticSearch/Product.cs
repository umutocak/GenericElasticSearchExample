using GenericElasticSearchExample.Core.Entities;

namespace GenericElasticSearchExample.Entities.DTOs.ElasticSearch
{
    public class Product : IElasticsearchModal
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
