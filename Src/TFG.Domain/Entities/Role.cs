namespace TFG.Domain.Entities;

public class Role : IEntity
{
    public string Id { get ; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get ; set ; }
    public DateTime? LastModifiedAt { get ; set ; }
}