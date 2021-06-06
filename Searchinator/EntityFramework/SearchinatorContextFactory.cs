namespace Searchinator.EntityFramework
{
    using Searchinator.Configuration;

    public class SearchinatorContextFactory : ISearchinatorContextFactory
    {
        private readonly ISearchinatorConfiguration searchinatorConfiguration;

        public SearchinatorContextFactory(ISearchinatorConfiguration searchinatorConfiguration)
        {
            this.searchinatorConfiguration = searchinatorConfiguration;
        }

        public ISearchinatorContext GetSearchinatorContext()
        {
            return new SearchinatorContext(this.searchinatorConfiguration.ConnectionString);
        }
    }
}