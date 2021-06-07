namespace Searchinator.IntegrationTests.Customizations
{
    using AutoFixture;

    using Searchinator.Models;

    public class PersonCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Person>(c => c.With(p => p.Id, 0));
        }
    }
}