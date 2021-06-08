namespace Searchinator.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Searchinator.Entities;
    using Searchinator.EntityFramework;
    using Searchinator.Models;

    public class InterestRepository : IInterestRepository
    {
        private readonly ISearchinatorContextFactory searchinatorContextFactory;

        public InterestRepository(ISearchinatorContextFactory searchinatorContextFactory)
        {
            this.searchinatorContextFactory = searchinatorContextFactory;
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

        public IList<Interest> GetInterestsForPerson(int personId)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            return context.Interests.Include(i => i.PersonEntity)
                .Where(i => i.PersonEntity.Id == personId)
                .ToList()
                .Select(this.ToModel)
                .ToList();
        }

        public IList<Interest> GetAllInterests()
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            return context.Interests.Include(i => i.PersonEntity).ToList().Select(this.ToModel).ToList();
        }

        public Interest SaveInterest(Interest interest)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            var personEntity = context.People.FirstOrDefault(p => p.Id == interest.PersonId);

            if (personEntity is null)
            {
                throw new InvalidOperationException("Unable to add interest if interest is not linked to a person.");
            }


            var interestEntity = context.Interests.FirstOrDefault(i => i.Id == interest.Id);

            var entityToSave = interestEntity ?? new InterestEntity();
            entityToSave.Id = interest.Id;
            entityToSave.Description = interest.Description;
            entityToSave.PersonEntity = personEntity;

            var savedInterestEntity = interestEntity is null ? context.Add(entityToSave) : entityToSave;
            
            context.SaveChanges();
            return this.ToModel(savedInterestEntity);
        }

        private Interest ToModel(InterestEntity interestEntity)
        {
            return new()
            {
                Id = interestEntity.Id,
                Description = interestEntity.Description,
                PersonId = interestEntity.PersonEntity.Id
            };
        }
    }
}