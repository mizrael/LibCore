using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibCore.Mongo
{

    public interface IRepository<TEntity>
    {
        void CreateIndex(IndexKeysDefinition<TEntity> indexDefinition);
        void CreateIndex(IndexKeysDefinition<TEntity> indexDefinition, CreateIndexOptions options);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task InsertOneAsync(TEntity entity);
        Task<TEntity> UpsertOneAsync(Expression<Func<TEntity, bool>> filter, TEntity entity);

        string CollectionName { get; }
    }

}
