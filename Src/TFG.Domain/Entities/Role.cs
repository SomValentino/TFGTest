using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TFG.Domain.Entities;

public class Role : IEntity {
    [BsonId]
    [BsonRepresentation (BsonType.ObjectId)]
    public string Id { get; set; }
    /// <summary>
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}