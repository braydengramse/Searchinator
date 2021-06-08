namespace Searchinator.Repositories
{
    using System.Collections.Generic;

    using Searchinator.Entities;
    using Searchinator.Models;

    public interface IInterestRepository
    {
        IList<Interest> GetInterestsForPerson(int personId);

        IList<Interest> GetInterests();

        Interest SaveInterest(Interest interest);

        void DeleteInterest(int interestId);
    }
}