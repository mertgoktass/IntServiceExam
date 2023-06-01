namespace exam.Infrastracture
{
    public class ProductRepository
    {
        private readonly YourDbContext _context;

        public ProductRepository(YourDbContext context)
        {
            public class ProductRepository
        {
            private readonly YourDbContext _context;

            public ProductRepository(YourDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Product> GetAllProducts()
            {
                return _context.Products.ToList();
            }

            public Product GetProductById(int id)
            {
                return _context.Products.FirstOrDefault(p => p.Id == id);
            }

            public void CreateProduct(Product product)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }

            public void UpdateProduct(Product product)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            }

            public void DeleteProduct(Product product)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
    }
}
