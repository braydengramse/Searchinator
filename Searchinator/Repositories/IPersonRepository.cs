namespace Searchinator.Repositories
{
    using System.Collections.Generic;

    using Searchinator.Entities;
    using Searchinator.Models;

    public interface IPersonRepository
    {
        IList<Person> GetPeople();

        Person? GetPerson(int personId);

        PersonEntity? GetPersonEntity(int personId);

        Person SavePerson(Person person);

        IList<Person> SearchPeople(string searchInput);

        void DeletePerson(int personId);
    }
}