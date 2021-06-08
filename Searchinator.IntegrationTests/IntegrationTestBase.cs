namespace Searchinator.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using AutoFixture;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using Searchinator.Bootstrapping;
    using Searchinator.Configuration;
    using Searchinator.IntegrationTests.Customizations;
    using Searchinator.IntegrationTests.TestHelpers;
    using Searchinator.Models;
    using Searchinator.Repositories;

    [TestFixture]
    public class IntegrationTestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }

        protected IPersonRepository PersonRepository { get; private set; }

        protected IInterestRepository InterestRepository { get; private set; }

        protected IFixture Fixture { get; private set; }

        [SetUp]
        public void TestSetUp()
        {
            this.PersonRepository = this.ServiceProvider.GetRequiredService<IPersonRepository>();
            this.InterestRepository = this.ServiceProvider.GetRequiredService<IInterestRepository>();
            this.Fixture.Customize(new PersonCustomization()).Customize(new InterestCustomization());
        }

        [TearDown]
        public void TestTearDown()
        {
            foreach (var interest in this.InterestRepository.GetAllInterests())
            {
                this.InterestRepository.DeleteInterest(interest.Id);
            }

            foreach (var person in this.PersonRepository.GetPeople())
            {
                this.PersonRepository.DeletePerson(person.Id);
            }
        }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            var services = new ServiceCollection();
            services.AddSearchinator(this.GetSearchinatorConfigurationManager());
            this.ServiceProvider = services.BuildServiceProvider();

            this.Fixture = new Fixture();
        }

        private SearchinatorConfigurationManager GetSearchinatorConfigurationManager()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            return new SearchinatorConfigurationManager(config);
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            using var process = Process.Start("sqllocaldb", "stop MSSQLLocalDB");
            if (process is null)
            {
                throw new InvalidOperationException("Command to stop localdb didn't produce a process.");
            }

            process.WaitForExit();
        }

        public IReadOnlyCollection<Person> AddAndRetrievePeopleSeed()
        {
            var peopleWithIdsFromDatabase =
                SeedData.People.Select(person => this.PersonRepository.SavePerson(person)).ToList();
            return peopleWithIdsFromDatabase;
        }

        public IReadOnlyCollection<Interest> AddAndRetrieveInterestsForPeopleSeed(IReadOnlyCollection<Person> people)
        {
            var interestsWithIdsFromDatabase = new List<Interest>();
            foreach (var person in people)
            {
                foreach (var interest in SeedData.Interests)
                {
                    interest.PersonId = person.Id;
                    interestsWithIdsFromDatabase.Add(this.InterestRepository.SaveInterest(interest));
                }
            }

            return interestsWithIdsFromDatabase;
        }
    }
}