namespace Searchinator.EntityFramework
{
    using System;
    using System.Linq;

    using Searchinator.Entities;

    public interface ISearchinatorContext : IDisposable
    {
        IQueryable<PersonEntity> People { get; }

        IQueryable<InterestEntity> Interests { get; }

        TEntity Add<TEntity>(TEntity entity)
            where TEntity : class;

        TEntity Remove<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}