namespace RepositorioApp.Utilities.Persistence
{
    public interface IRepository<T> :
        ICrudRepository<T>,
        IQueryRepository<T>,
        IQueryAsNoTrackingRepository<T>,
        IQueryAsNoTrackingWithIdentityResolutionRepository<T>
        where T : class
    {
    }
}
