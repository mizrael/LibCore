using System;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LibCore.Mongo
{

    public class Repository<TEntity> : IRepository<TEntity>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public Repository(IMongoCollection<TEntity> collection)
        {
            if (null == collection)
                throw new ArgumentNullException(nameof(collection));
            _collection = collection;

            this.CollectionName = collection.CollectionNamespace.CollectionName;
        }

        public void CreateIndex(IndexKeysDefinition<TEntity> indexDefinition)
        {
            CreateIndex(indexDefinition, null);
        }

        public void CreateIndex(IndexKeysDefinition<TEntity> indexDefinition, CreateIndexOptions options)
        {
            var db = _collection.Database;
            if (db == null)
                return;
            var coll = db.GetCollection<TEntity>(this.CollectionName);
            if (coll == null)
                return;
            coll.Indexes.CreateOne(indexDefinition, options);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, PagingOptions pagingOptions = null)
        {
            var options = new FindOptions<TEntity>();
            if(null != pagingOptions)
            {
                options.Limit = pagingOptions.PageSize;
                options.Skip = pagingOptions.Offset;
            }

            var cursor = await _collection.FindAsync(filter, options);

            return await cursor.ToListAsync();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var cursor = await _collection.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _collection.CountAsync(filter);
        }

        public Task InsertOneAsync(TEntity entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task<TEntity> UpsertOneAsync(Expression<Func<TEntity, bool>> filter, TEntity entity)
        {
            return _collection.FindOneAndReplaceAsync(filter, entity,
                                                      new FindOneAndReplaceOptions<TEntity, TEntity>() { IsUpsert = true });
        }

        public string CollectionName { get; private set; }
    }

}