namespace Searchinator.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Searchinator.Entities;

    public class SearchinatorContext : DbContext, ISearchinatorContext
    {
        public SearchinatorContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            if (connectionString.Contains("SearchinatorTests", StringComparison.OrdinalIgnoreCase))
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<SearchinatorContext>());
            }
            else
            {
                Database.SetInitializer(new SearchinatorDatabaseInitializer());
            }
        }

        public DbSet<PersonEntity> PeopleSet { get; set; }

        public DbSet<InterestEntity> InterestSet { get; set; }

        public IQueryable<InterestEntity> Interests => this.InterestSet;

        public IQueryable<PersonEntity> People => this.PeopleSet;

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