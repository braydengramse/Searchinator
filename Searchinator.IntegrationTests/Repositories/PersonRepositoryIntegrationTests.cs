namespace Searchinator.IntegrationTests.Repositories
{
    using System.Collections.Generic;

    using AutoFixture;

    using FluentAssertions;

    using NUnit.Framework;

    using Searchinator.Models;

    public class PersonRepositoryIntegrationTests : IntegrationTestBase
    {
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
            this.AddAndRetrieveInterestsForPeopleSeed(peopleSeed);

            // Act
            var searchResults = this.PersonRepository.SearchPeople("basketball");

            // Assert
            searchResults.Should().BeEquivalentTo(peopleSeed);
        }

        [Test]
        public void ShouldDeletePersonAndAssociatedInterests()
        {
            // Arrange
            var person = this.Fixture.Create<Person>();
            var savedPerson = this.PersonRepository.SavePerson(person);
            var interests = new List<Interest> { this.Fixture.Create<Interest>(), this.Fixture.Create<Interest>() };
            foreach (var interest in interests)
            {
                interest.PersonId = savedPerson.Id;
                this.InterestRepository.SaveInterest(interest);
            }

            // Act
            this.PersonRepository.DeletePerson(savedPerson.Id);

            // Assert
            this.InterestRepository.GetInterestsForPerson(savedPerson.Id).Should().BeEmpty();
            this.PersonRepository.GetPerson(savedPerson.Id).Should().BeNull();
        }
    }
}