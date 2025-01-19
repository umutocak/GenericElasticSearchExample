using GenericElasticSearchExample.Business.Abstract;
using GenericElasticSearchExample.Entities.DTOs.Products;
using Microsoft.AspNetCore.Mvc;

namespace GenericElasticSearchExample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall-products")]
        public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllProductsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _productService.GetById(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet("get-product-details")]
        public async Task<IActionResult> GetProductDetails(CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductDetails(cancellationToken);
            return Ok(result);
        }

        [HttpGet("match")]
        public async Task<IActionResult> Match(string queryKeyword, CancellationToken cancellationToken)
        {
            var result = await _productService.Match(queryKeyword, cancellationToken);
            return Ok(result);
        }

        [HttpGet("fuzzy")]
        public async Task<IActionResult> Fuzzy(string queryKeyword, CancellationToken cancellationToken)
        {
            var result = await _productService.Fuzzy(queryKeyword, cancellationToken);
            return Ok(result);
        }

        [HttpGet("wildcard")]
        public async Task<IActionResult> Wildcard(string queryKeyword, CancellationToken cancellationToken)
        {
            var result = await _productService.Wildcard(queryKeyword, cancellationToken);
            return Ok(result);
        }

        [HttpGet("exists")]
        public async Task<IActionResult> Exists(CancellationToken cancellationToken)
        {
            var result = await _productService.Exists(cancellationToken);
            return Ok(result);
        }

        [HttpGet("bool")]
        public async Task<IActionResult> Bool(CancellationToken cancellationToken)
        {
            var result = await _productService.Bool(cancellationToken);
            return Ok(result);
        }

        [HttpGet("term")]
        public async Task<IActionResult> Term(string queryKeyword, CancellationToken cancellationToken)
        {
            var result = await _productService.Term(queryKeyword, cancellationToken);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count(CancellationToken cancellationToken)
        {
            var result = await _productService.Count(cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            var result = await _productService.Create(createProductDto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var result =  await _productService.Update(updateProductDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var result = await _productService.Delete(id, cancellationToken);
            return Ok(result);
        }


    }
}
