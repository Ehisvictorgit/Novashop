using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class User : BaseEntity
{
    [BsonElement("usr")]
    public string Usr { get; set; }
    [BsonElement("psw")]
    public string Psw { get; set; }
}
