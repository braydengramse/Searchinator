namespace Searchinator.Repositories
{
    using System.Collections.Generic;

    using Searchinator.Entities;
    using Searchinator.Models;

    public interface IInterestRepository
    {
        IList<Interest> GetInterestsForPerson(int personId);

        Interest SaveInterest(Interest interest, PersonEntity personEntity);

        void DeleteInterest(int interestId);
    }
}