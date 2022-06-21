using MongoDB.Driver;
using TFG.Application.Contracts.Persistence;
using TFG.Domain.Entities;

namespace TFG.Infrastructure.Data;

public class RoleDataContext : IDataContext<Role> {
    public RoleDataContext (string connectionString, string database, string collectionName) 
    {
        Client = new MongoClient (connectionString);
        Database = Client.GetDatabase (database);
        Collections = Database.GetCollection<Role> (collectionName);
    }
    public IMongoCollection<Role> Collections { get; set; }
    public IMongoClient Client { get; set; }
    public IMongoDatabase Database { get; set; }
}