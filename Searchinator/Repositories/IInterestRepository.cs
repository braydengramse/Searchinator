namespace Searchinator.Repositories
{
    using System.Collections.Generic;

    using Searchinator.Models;

    public interface IInterestRepository
    {
        IList<Interest> GetInterests();

        void AddInterest(Interest interest);

        void DeleteInterest(int interestId);
    }
}
