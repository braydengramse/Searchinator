namespace Searchinator.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Searchinator.EntityFramework;
    using Searchinator.Models;

    public class InterestRepository : IInterestRepository
    {
        private readonly ISearchinatorContextFactory searchinatorContextFactory;

        public InterestRepository(ISearchinatorContextFactory searchinatorContextFactory)
        {
            this.searchinatorContextFactory = searchinatorContextFactory;
        }

        public IList<Interest> GetInterests()
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            return context.Interests.Include(i => i.Person).ToList();
        }

        public void AddInterest(Interest interest)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            context.Add(interest);
            context.SaveChanges();
        }

        public void DeleteInterest(int interestId)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            var interest = context.Interests.FirstOrDefault(i => i.Id == interestId);

            if (interest is null)
            {
                return;
            }

            context.Remove(interest);
            context.SaveChanges();
        }
    }
}