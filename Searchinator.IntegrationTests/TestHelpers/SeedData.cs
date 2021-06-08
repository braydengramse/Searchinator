namespace Searchinator.IntegrationTests.TestHelpers
{
    using System.Collections.Generic;

    using Searchinator.Models;

    public static class SeedData
    {
        private static readonly string VivintSmartHomeArenaAddress = "301 S Temple, Salt Lake City, UT 84101";

        public static IList<Person> People = new List<Person>
        {
            new() { Name = "Donovan Mitchell", Age = 24, Address = VivintSmartHomeArenaAddress },
            new() { Name = "Rudy Gobert", Age = 28, Address = VivintSmartHomeArenaAddress },
            new() { Name = "Bojan Bogdanovic", Age = 32, Address = VivintSmartHomeArenaAddress },
            new() { Name = "Mike Conley", Age = 33, Address = VivintSmartHomeArenaAddress },
            new() { Name = "Royce O'Neale", Age = 28, Address = VivintSmartHomeArenaAddress }
        };

        public static IList<Interest> Interests = new List<Interest>
        {
            new() { Description = "playing basketball" }, new() { Description = "winning the NBA finals" }
        };
    }
}