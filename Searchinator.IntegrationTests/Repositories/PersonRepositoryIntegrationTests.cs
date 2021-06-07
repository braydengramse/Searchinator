namespace Searchinator.IntegrationTests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture;

    using FluentAssertions;

    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using Searchinator.IntegrationTests.Customizations;
    using Searchinator.Models;
    using Searchinator.Repositories;

    public class PersonRepositoryIntegrationTests : IntegrationTestBase
    {
        protected IPersonRepository PersonRepository { get; private set; }

        protected IInterestRepository InterestRepository { get; private set; }

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
            foreach (var interest in this.InterestRepository.GetInterests())
            {
                this.InterestRepository.DeleteInterest(interest.Id);
            }

            foreach (var person in this.PersonRepository.GetPeople())
            {
                this.PersonRepository.DeletePerson(person.Id);
            }
        }
        
        [Test]
        public void ShouldGetAllPeople()
        {
            // Arrange
            var peopleSeed = this.AddAndRetrievePeopleSeed();

            // Act
            var peopleResult = this.PersonRepository.GetPeople();

            // Assert
            peopleResult.Should().BeEquivalentTo(peopleSeed);
        }

        [Test]
        public void ShouldGetExistingPersonById()
        {
            // Arrange
            var personToAdd = this.Fixture.Create<Person>();
            var actualPerson = this.PersonRepository.SavePerson(personToAdd);

            // Act
            var resultPerson = this.PersonRepository.GetPerson(actualPerson.Id);

            // Assert
            resultPerson.Should().BeEquivalentTo(actualPerson);
        }

        [Test]
        public void ShouldBeAbleToAddAPerson()
        {
            // Arrange
            var personToAdd = this.Fixture.Create<Person>();

            // Act
            var actualPerson = this.PersonRepository.SavePerson(personToAdd);

            // Assert
            actualPerson.Id.Should().NotBe(personToAdd.Id);
            actualPerson.Id.Should().NotBe(0);
            actualPerson.Name.Should().Be(personToAdd.Name);
            actualPerson.Address.Should().Be(personToAdd.Address);
            actualPerson.Age.Should().Be(personToAdd.Age);
        }

        [Test]
        public void ShouldBeAbleToUpdateAnExistingPerson()
        {
            // Arrange
            var personToAdd = this.Fixture.Create<Person>();
            var actualPerson = this.PersonRepository.SavePerson(personToAdd);
            var personToUpdate = this.PersonRepository.GetPerson(actualPerson.Id);
            personToUpdate.Should().NotBeNull();
            personToUpdate.Name = this.Fixture.Create<string>();

            // Act
            this.PersonRepository.SavePerson(personToUpdate);
            var updatedPerson = this.PersonRepository.GetPerson(actualPerson.Id);

            // Assert
            updatedPerson.Should().NotBeNull();
            updatedPerson.Name.Should().NotBe(personToAdd.Name);
            updatedPerson.Name.Should().Be(personToUpdate.Name);
        }

        [Test]
        public void ShouldReturnCorrectSearchResultsMatchingInterests()
        {
            // Arrange
            var peopleSeed = this.AddAndRetrievePeopleSeed();
            this.AddInterestsForPeopleSeed(peopleSeed);

            // Act
            var searchResults = this.PersonRepository.SearchPeople("basketball");

            // Assert
            searchResults.Should().BeEquivalentTo(peopleSeed);
        }

        private IReadOnlyCollection<Person> AddAndRetrievePeopleSeed()
        {
            var vivintSmartHomeArenaAddress = "301 S Temple, Salt Lake City, UT 84101";
            var people = new List<Person>
            {
                new() { Name = "Donovan Mitchell", Age = 24, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Rudy Gobert", Age = 28, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Bojan Bogdanovic", Age = 32, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Mike Conley", Age = 33, Address = vivintSmartHomeArenaAddress },
                new() { Name = "Royce O'Neale", Age = 28, Address = vivintSmartHomeArenaAddress }
            };
            var peopleWithIdsFromDatabase = people.Select(person => this.PersonRepository.SavePerson(person)).ToList();
            return peopleWithIdsFromDatabase;
        }

        private void AddInterestsForPeopleSeed(IReadOnlyCollection<Person> people)
        {
            var basketballInterestDescription = "playing basketball";
            var nbaFinalsInterestDescription = "winning the NBA finals";

            foreach (var person in people)
            {
                var interestsToAdd = new List<Interest>
                {
                    new() { Description = basketballInterestDescription, PersonId = person.Id},
                    new() { Description = nbaFinalsInterestDescription, PersonId = person.Id }
                };

                var personEntity = this.PersonRepository.GetPersonEntity(person.Id);

                if (personEntity is null)
                {
                    throw new InvalidOperationException($"Person entity not found for person id {person.Id}.");
                }

                foreach (var interest in interestsToAdd)
                {
                    var interestFromDatabase = this.InterestRepository.SaveInterest(interest, personEntity);
                }
            }
        }
    }
}