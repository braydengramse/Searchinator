namespace Searchinator.EntityFramework
{
    public interface ISearchinatorContextFactory
    {
        ISearchinatorContext GetSearchinatorContext();
    }
}
