namespace Searchinator.IntegrationTests.Repositories
{
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

        [SetUp]
        public void TestSetUp()
        {
            this.PersonRepository = this.ServiceProvider.GetRequiredService<IPersonRepository>();
            this.Fixture.Customize(new PersonCustomization());
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
    }
}