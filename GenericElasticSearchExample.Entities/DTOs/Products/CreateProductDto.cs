using GenericElasticSearchExample.Core.Entities;

namespace GenericElasticSearchExample.Entities.DTOs.Products
{
    public class CreateProductDto : IDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
