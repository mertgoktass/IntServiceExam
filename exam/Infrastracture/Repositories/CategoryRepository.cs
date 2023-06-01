namespace exam.Infrastracture.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext _dbContext;

        public CategoryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public Category GetById(int categoryId)
        {
            return _dbContext.Categories.Find(categoryId);
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }

}
