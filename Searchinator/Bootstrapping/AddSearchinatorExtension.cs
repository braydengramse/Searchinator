namespace Searchinator.Bootstrapping
{
    using Microsoft.Extensions.DependencyInjection;

    using Searchinator.Configuration;
    using Searchinator.EntityFramework;
    using Searchinator.Repositories;

    public static class AddSearchinatorExtension
    {
        public static void AddSearchinator(this IServiceCollection services, SearchinatorConfigurationManager searchinatorConfigurationManager)
        {
            services.AddSingleton<ISearchinatorConfiguration>(searchinatorConfigurationManager);
            services.AddTransient<ISearchinatorContextFactory, SearchinatorContextFactory>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IInterestRepository, InterestRepository>();
        }
    }
}
