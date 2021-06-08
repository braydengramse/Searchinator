namespace Searchinator.IntegrationTests.Repositories
{
    using System;
    using System.Linq;

    using AutoFixture;

    using FluentAssertions;

    using NUnit.Framework;

    using Searchinator.Models;

    public class InterestRepositoryIntegrationTests : IntegrationTestBase
    {
        [Test]
        public void ShouldGetAllInterests()
        {
            // Arrange
            var peopleSeed = this.AddAndRetrievePeopleSeed();
            var expectedInterests = this.AddAndRetrieveInterestsForPeopleSeed(peopleSeed);

            // Act
            var actualInterests = this.InterestRepository.GetAllInterests();

            // Assert
            actualInterests.Should().BeEquivalentTo(expectedInterests);
        }

        [Test]
        public void ShouldGetInterestsForPerson()
        {
            // Arrange
            var peopleSeed = this.AddAndRetrievePeopleSeed();
            var interests = this.AddAndRetrieveInterestsForPeopleSeed(peopleSeed);
            var person = peopleSeed.First();
            var expectedInterests = interests.Where(i => i.PersonId == person.Id);

            // Act
            var actualInterests = this.InterestRepository.GetInterestsForPerson(person.Id);

            // Assert
            actualInterests.Should().BeEquivalentTo(expectedInterests);
        }

        [Test]
        public void ShouldDeleteInterests()
        {
            // Arrange
            var peopleSeed = this.AddAndRetrievePeopleSeed();
            var interestSeed = this.AddAndRetrieveInterestsForPeopleSeed(peopleSeed);
            var personWithNoMoreInterests = peopleSeed.First();

            // Act
            foreach (var interestToDelete in interestSeed.Where(i => i.PersonId == personWithNoMoreInterests.Id))
            {
                this.InterestRepository.DeleteInterest(interestToDelete.Id);
            }

            // Assert
            this.InterestRepository.GetInterestsForPerson(personWithNoMoreInterests.Id).Should().BeEmpty();
        }

        [Test]
        public void ShouldBeAbleToUpdateAnExistingInterest()
        {
            // Arrange
            var personToAdd = this.Fixture.Create<Person>();
            var addedPerson = this.PersonRepository.SavePerson(personToAdd);
            var interestToAdd = this.Fixture.Create<Interest>();
            interestToAdd.PersonId = addedPerson.Id;
            var addedInterest = this.InterestRepository.SaveInterest(interestToAdd);
            var interestToUpdate = this.InterestRepository.GetInterestsForPerson(addedPerson.Id).Single();
            interestToUpdate.Description = this.Fixture.Create<string>();

            // Act
            this.InterestRepository.SaveInterest(interestToUpdate);
            var updatedInterest = this.InterestRepository.GetInterestsForPerson(addedPerson.Id).Single();

            // Assert
            updatedInterest.Description.Should().NotBe(addedInterest.Description);
            updatedInterest.Description.Should().Be(interestToUpdate.Description);
        }

        [Test]
        public void ShouldBeAbleToAddAnInterest()
        {
            // Arrange
            var personToAdd = this.Fixture.Create<Person>();
            var addedPerson = this.PersonRepository.SavePerson(personToAdd);
            var interestToAdd = this.Fixture.Create<Interest>();
            interestToAdd.PersonId = addedPerson.Id;

            // Act
            var actualInterest = this.InterestRepository.SaveInterest(interestToAdd);

            // Assert
            actualInterest.Id.Should().NotBe(interestToAdd.Id);
            actualInterest.Id.Should().NotBe(0);
            actualInterest.Description.Should().Be(interestToAdd.Description);
            actualInterest.PersonId.Should().Be(interestToAdd.PersonId);
        }

        [Test]
        public void ShouldThrowExceptionIfInterestHasNoRelatedPerson()
        {
            // Arrange
            var interestToAdd = this.Fixture.Create<Interest>();

            // Act
            Action action = () => this.InterestRepository.SaveInterest(interestToAdd);

            // Assert
            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Unable to add interest if interest is not linked to a person.");
        }
    }
}