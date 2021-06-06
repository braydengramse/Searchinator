namespace Searchinator.EntityFramework
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Searchinator.Models;

    public class SearchinatorDatabaseInitializer : CreateDatabaseIfNotExists<SearchinatorContext>
    {
        protected override void Seed(SearchinatorContext context)
        {
            var vivintSmartHomeArenaAddress = "301 S Temple, Salt Lake City, UT 84101";
            var basketballInterestDescription = "playing basketball";
            var nbaFinalsInterestDescription = "winning the NBA finals";

            var people = new List<Person>
            {
                new()
                {
                    Name = "Donovan Mitchell",
                    Age = 24,
                    Address = vivintSmartHomeArenaAddress,
                    Interests = new List<Interest>
                    {
                        new() { Description = basketballInterestDescription },
                        new() { Description = nbaFinalsInterestDescription }
                    }
                },
                new()
                {
                    Name = "Rudy Gobert",
                    Age = 28,
                    Address = vivintSmartHomeArenaAddress,
                    Interests = new List<Interest>
                    {
                        new() { Description = basketballInterestDescription },
                        new() { Description = nbaFinalsInterestDescription }
                    }
                },
                new()
                {
                    Name = "Bojan Bogdanovic",
                    Age = 32,
                    Address = vivintSmartHomeArenaAddress,
                    Interests = new List<Interest>
                    {
                        new() { Description = basketballInterestDescription },
                        new() { Description = nbaFinalsInterestDescription }
                    }
                },
                new()
                {
                    Name = "Mike Conley",
                    Age = 33,
                    Address = vivintSmartHomeArenaAddress,
                    Interests = new List<Interest>
                    {
                        new() { Description = basketballInterestDescription },
                        new() { Description = nbaFinalsInterestDescription }
                    }
                },
                new()
                {
                    Name = "Royce O'Neale",
                    Age = 28,
                    Address = vivintSmartHomeArenaAddress,
                    Interests = new List<Interest>
                    {
                        new() { Description = basketballInterestDescription },
                        new() { Description = nbaFinalsInterestDescription }
                    }
                }
            };

            context.PeopleSet.AddRange(people);

            base.Seed(context);
        }
    }
}