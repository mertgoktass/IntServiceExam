namespace exam.Application.Services
{
    public class ProductService
    {
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

        {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void CreateProduct(Product product)
        {
            ValidateProduct(product);
            _productRepository.CreateProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            ValidateProduct(product);
            _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            _productRepository.DeleteProduct(product);
        }
        private ValidationResult ValidateShoppingCart(List<Product> products)
        {
           
            var groupedProducts = products.GroupBy(p => p.CategoryId);

            if (!groupedProducts.Any(g => g.Count() >= 2))
            {
                return new ValidationResult(false, "Invalid shopping cart. At least two products of the same category are required for a discount.");
            }

            return ValidationResult.Valid;
        }
        private decimal CalculateDiscountAmount(List<Product> products)
        {
           
            var discountAmount = 0m;

            var groupedProducts = products.GroupBy(p => p.CategoryId);

            foreach (var group in groupedProducts)
            {
                var firstProduct = group.First();
                discountAmount += firstProduct.Price * 0.05m;
            }

            return discountAmount;
        }

        private void ValidateProduct(Product product)
        {
            

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new Exception("Product name is required.");
            }

            if (product.Price <= 0)
            {
                throw new Exception("Product price must be a positive value.");
            }

            
        }
    }
    }
}
