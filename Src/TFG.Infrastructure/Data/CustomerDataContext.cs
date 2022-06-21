using MongoDB.Driver;
using TFG.Application.Contracts.Persistence;
using TFG.Domain.Entities;

namespace TFG.Infrastructure.Data;

public class CustomerDataContext : IDataContext<Customer> {
    public CustomerDataContext (string connectionString, string database, string collectionName) 
    {
        Client = new MongoClient (connectionString);
        Database = Client.GetDatabase (database);
        Collections = Database.GetCollection<Customer> (collectionName);
    }
    public IMongoCollection<Customer> Collections { get; set; }
    public IMongoClient Client { get; set; }
    public IMongoDatabase Database { get; set; }
}