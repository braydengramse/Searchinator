namespace Searchinator.IntegrationTests.Customizations
{
    using AutoFixture;

    using Searchinator.Models;

    internal class InterestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Interest>(c => c.With(p => p.Id, 0));
        }
    }
}