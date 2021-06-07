namespace Searchinator.IntegrationTests
{
    using System;
    using System.Diagnostics;

    using AutoFixture;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using Searchinator.Bootstrapping;
    using Searchinator.Configuration;

    [TestFixture]
    public class IntegrationTestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }

        protected IFixture Fixture { get; private set; }

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
    }
}
