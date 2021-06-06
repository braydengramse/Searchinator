namespace Searchinator.EntityFramework
{
    using System.Data.Entity;
    using System.Linq;

    using Searchinator.Models;

    public class SearchinatorContext : DbContext, ISearchinatorContext
    {
        public SearchinatorContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new SearchinatorDatabaseInitializer());
        }

        public DbSet<Person> PeopleSet { get; set; }

        public DbSet<Interest> InterestSet { get; set; }

        public IQueryable<Interest> Interests => this.InterestSet;

        public IQueryable<Person> People => this.PeopleSet;

        public TEntity Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            var dbSet = this.Set<TEntity>();
            return dbSet.Add(entity);
        }

        public TEntity Remove<TEntity>(TEntity entity)
            where TEntity : class
        {
            var dbSet = this.Set<TEntity>();
            return dbSet.Remove(entity);
        }
    }
}