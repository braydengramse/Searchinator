namespace Searchinator.EntityFramework
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Searchinator.Entities;

    public class SearchinatorDatabaseInitializer : CreateDatabaseIfNotExists<SearchinatorContext>
    {
        protected override void Seed(SearchinatorContext context)
        {
            var vivintSmartHomeArenaAddress = "301 S Temple, Salt Lake City, UT 84101";
            var basketballInterestDescription = "playing basketball";
            var nbaFinalsInterestDescription = "winning the NBA finals";

            var people = new List<PersonEntity>
            {
                new() { Name = "Donovan Mitchell", Age = 24, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Rudy Gobert", Age = 28, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Bojan Bogdanovic", Age = 32, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Mike Conley", Age = 33, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Royce O'Neale", Age = 28, Address = vivintSmartHomeArenaAddress }
            };

            context.PeopleSet.AddRange(people);
            context.SaveChanges();

            var interests = new List<InterestEntity>();

            foreach (var personEntity in people)
            {
                interests.AddRange(
                    new List<InterestEntity>
                    {
                        new() { Description = basketballInterestDescription, PersonEntity = personEntity },
                        new() { Description = nbaFinalsInterestDescription, PersonEntity = personEntity }
                    });
            }

            context.InterestSet.AddRange(interests);
            base.Seed(context);
        }
    }
}