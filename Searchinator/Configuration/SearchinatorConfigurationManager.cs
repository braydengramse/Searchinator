namespace Searchinator.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class SearchinatorConfigurationManager : ISearchinatorConfiguration
    {
        public SearchinatorConfigurationManager(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string ConnectionString => this.Configuration.GetValue<string>("ConnectionString");
    }
}