using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace exam.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("calculate-discount")]
        public IActionResult CalculateDiscount([FromQuery] List<int> productIds)
        {
            try
            {
               
                var products = _productRepository.GetByIds(productIds);

              
                var validationResult = ValidateShoppingCart(products);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ErrorMessage);
                }

                var discount = CalculateDiscountAmount(products);

                return Ok(discount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during the discount calculation.");
            }
        }



        [HttpPost]
        [Route("import")]
        public IActionResult ImportProductStock([FromBody] List<ImportProductDto> importedProducts)
        {
            try
            {
                foreach (var importedProduct in importedProducts)
                {
                   
                    var existingProduct = _productRepository.GetByName(importedProduct.Name);

                    if (existingProduct == null)
                    {
                        // Create a new product
                        var newProduct = new Product
                        {
                            Name = importedProduct.Name,
                            Description = importedProduct.Description,
                            Price = importedProduct.Price
                        };

                       
                        foreach (var categoryName in importedProduct.Categories)
                        {
                            var existingCategory = _categoryRepository.GetByName(categoryName);

                            if (existingCategory == null)
                            {
                             
                                var newCategory = new Category
                                {
                                    Name = categoryName
                                };

                               
                                _categoryRepository.Create(newCategory);

                              
                                newProduct.Categories.Add(newCategory);
                            }
                            else
                            {
                                
                                newProduct.Categories.Add(existingCategory);
                            }
                        }

                       
                        _productRepository.Create(newProduct);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during the import process.");
            }
        }
    }
    private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult CreateProduct(ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errors);
            }

            var createdProduct = _productService.CreateProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errors);
            }

            var updatedProduct = _productService.UpdateProduct(id, productDto);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deletedProduct = _productService.DeleteProduct(id);
            if (deletedProduct == null)
                return NotFound();

            return Ok(deletedProduct);




        }
    }

}
