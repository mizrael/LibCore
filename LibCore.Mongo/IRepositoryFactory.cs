namespace LibCore.Mongo
{

    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>(RepositoryOptions options);
    }

}