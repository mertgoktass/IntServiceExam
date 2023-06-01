using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class ProductService
    {
        [Test]
        public void CreateProduct_ValidProduct_ReturnsCreatedProduct()
        {

            var productService = new ProductService();
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 9.99,
                Category = "Test Category"
            };

  
            var result = productService.CreateProduct(product);

 
            Assert.IsNotNull(result);
            
        }
    }
}
