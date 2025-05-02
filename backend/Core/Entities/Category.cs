using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class Category : BaseEntity
{
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("icon")]
    public string Icon { get; set; }
}
