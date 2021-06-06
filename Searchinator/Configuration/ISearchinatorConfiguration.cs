using Microsoft.Extensions.Configuration;

namespace Searchinator.Configuration
{
    public interface ISearchinatorConfiguration
    {
        IConfiguration Configuration { get; }

        string ConnectionString { get; }
    }
}
