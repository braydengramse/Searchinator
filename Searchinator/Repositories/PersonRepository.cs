namespace Searchinator.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;

    using Searchinator.EntityFramework;
    using Searchinator.Models;

    public class PersonRepository : IPersonRepository
    {
        private readonly IInterestRepository interestRepository;

        private readonly ISearchinatorContextFactory searchinatorContextFactory;

        public PersonRepository(
            ISearchinatorContextFactory searchinatorContextFactory,
            IInterestRepository interestRepository)
        {
            this.searchinatorContextFactory = searchinatorContextFactory;
            this.interestRepository = interestRepository;
        }

        public IList<Person> GetPeople()
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            return context.People.Include(p => p.Interests).ToList();
        }

        public Person? GetPerson(int personId)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            return context.People.Include(p => p.Interests).FirstOrDefault(p => p.Id == personId);
        }

        public void AddPerson(Person person)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            context.Add(person);
            context.SaveChanges();
        }

        public IList<Person> SearchPeople(string searchInput)
        {
            return this.GetPeople()
                .Where(
                    p =>
                        p.Id.ToString(CultureInfo.InvariantCulture)
                            .Contains(searchInput, StringComparison.OrdinalIgnoreCase)
                        || p.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase)
                        || p.Age.ToString(CultureInfo.InvariantCulture)
                            .Contains(searchInput, StringComparison.OrdinalIgnoreCase)
                        || p.Address.Contains(searchInput, StringComparison.OrdinalIgnoreCase)
                        || p.Interests.Any(
                            i => i.Description.Contains(searchInput, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        public void DeletePerson(int personId)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            var interests = this.interestRepository.GetInterests().Where(i => i.Person.Id == personId);

            foreach (var interest in interests)
            {
                this.interestRepository.DeleteInterest(interest.Id);
            }

            var person = context.People.Include(p => p.Interests).FirstOrDefault(p => p.Id == personId);

            if (person is null)
            {
                return;
            }

            context.Remove(person);
            context.SaveChanges();
        }
    }
}