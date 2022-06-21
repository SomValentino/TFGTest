using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TFG.Domain.Entities;

public class Customer : IEntity 
{
    [BsonId]
    [BsonRepresentation (BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public Role Role { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}