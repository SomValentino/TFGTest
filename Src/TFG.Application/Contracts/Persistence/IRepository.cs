using MongoDB.Bson;
using MongoDB.Driver;
using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Persistence;

public interface IRepository<TEntity> where TEntity : class, IEntity 
{
    Task<TEntity> Create (TEntity entity);
    Task<bool> Update (TEntity entity);
    Task<bool> Delete (TEntity entity);
    Task<IReadOnlyList<TEntity>> GetAsync ();
    Task<TEntity> GetAsync (string Id);
    Task<IReadOnlyList<TEntity>> GetAsync (FilterDefinition<BsonDocument> filter);
}