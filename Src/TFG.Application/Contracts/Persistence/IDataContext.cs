using MongoDB.Driver;
using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Persistence;

public interface IDataContext<TEntity> where TEntity : class, IEntity 
{
    public IMongoCollection<TEntity> Collections { get; set; }
    public IMongoClient Client { get; set; }
    public IMongoDatabase Database { get; set; }
}