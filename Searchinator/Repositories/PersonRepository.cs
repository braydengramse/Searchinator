namespace Searchinator.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;

    using Searchinator.Entities;
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
            return context.People.ToList().Select(this.ToModel).ToList();
        }

        public Person? GetPerson(int personId)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            var personEntity = context.People.FirstOrDefault(p => p.Id == personId);
            return personEntity is null ? null : this.ToModel(personEntity);
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
                        || this.interestRepository.GetInterestsForPerson(p.Id)
                            .Any(i => i.Description.Contains(searchInput, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        public void DeletePerson(int personId)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            var interestIds = context.Interests.Include(i => i.PersonEntity)
                .Where(i => i.PersonEntity.Id == personId)
                .Select(i => i.Id);

            foreach (var interestId in interestIds)
            {
                this.interestRepository.DeleteInterest(interestId);
            }

            var person = context.People.FirstOrDefault(p => p.Id == personId);

            if (person is null)
            {
                return;
            }

            context.Remove(person);
            context.SaveChanges();
        }

        public Person SavePerson(Person person)
        {
            using var context = this.searchinatorContextFactory.GetSearchinatorContext();
            var personEntity = context.People.FirstOrDefault(p => p.Id == person.Id);

            var entityToSave = personEntity ?? new PersonEntity();
            entityToSave.Id = person.Id;
            entityToSave.Address = person.Address;
            entityToSave.Age = person.Age;
            entityToSave.Name = person.Name;

            var savedPersonEntity = personEntity is null ? context.Add(entityToSave) : entityToSave;

            context.SaveChanges();
            return this.ToModel(savedPersonEntity);
        }

        private Person ToModel(PersonEntity personEntity)
        {
            return new()
            {
                Id = personEntity.Id,
                Address = personEntity.Address,
                Age = personEntity.Age,
                Name = personEntity.Name
            };
        }
    }
}