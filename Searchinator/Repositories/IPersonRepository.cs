namespace Searchinator.Repositories
{
    using System.Collections.Generic;

    using Searchinator.Models;

    public interface IPersonRepository
    {
        IList<Person> GetPeople();

        Person? GetPerson(int personId);
        
        void AddPerson(Person person);

        IList<Person> SearchPeople(string searchInput);

        void DeletePerson(int personId);
    }
}
