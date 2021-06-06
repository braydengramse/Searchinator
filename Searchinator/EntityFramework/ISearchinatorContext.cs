namespace Searchinator.EntityFramework
{
    using System;
    using System.Linq;

    using Searchinator.Models;

    public interface ISearchinatorContext : IDisposable
    {
        IQueryable<Person> People { get; }

        IQueryable<Interest> Interests { get; }

        TEntity Add<TEntity>(TEntity entity)
            where TEntity : class;

        TEntity Remove<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}