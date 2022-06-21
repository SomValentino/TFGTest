namespace TFG.Domain.Entities;

public interface IEntity {
    string Id {get; set;}
    DateTime CreatedAt {get; set;}
    DateTime LastModifiedAt {get; set;}
}