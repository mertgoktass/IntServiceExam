namespace exam.Infrastracture.Repositories
{
    public class ProductRepository
    {
        private readonly DbContext _dbContext;

        public ProductRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public Product GetById(int productId)
        {
            return _dbContext.Products.Find(productId);
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
