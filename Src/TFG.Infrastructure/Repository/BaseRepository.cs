using MongoDB.Bson;
using MongoDB.Driver;
using TFG.Application.Contracts.Persistence;
using TFG.Domain.Entities;

namespace TFG.Infrastructure.Repository;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity {
    protected readonly IDataContext<TEntity> _context;

    public BaseRepository (IDataContext<TEntity> context) {
        _context = context;
    }
    public async Task<TEntity> Create (TEntity entity) {
        await _context.Collections.InsertOneAsync (entity);

        return entity;
    }

    public async Task<bool> Delete (TEntity entity) {
        var filter = Builders<TEntity>.Filter.Eq (x => x.Id, entity.Id);

        var result = await _context.Collections.DeleteOneAsync (filter);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync () {
        return await _context.Collections.Find (p => true).ToListAsync ();
    }

    public async Task<TEntity> GetAsync (string Id) {
        var filter = Builders<TEntity>.Filter.Eq (x => x.Id, Id);

        return await _context.Collections.Find (filter).FirstOrDefaultAsync ();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync (FilterDefinition<TEntity> filter) {
        var data = await _context.Collections.Find (filter).ToListAsync ();

        return data;
    }

    public async Task<bool> Update (TEntity entity) 
    {
        var filter = Builders<TEntity>.Filter.Eq (x => x.Id, entity.Id);

        var result = await _context.Collections.ReplaceOneAsync (filter, entity);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}