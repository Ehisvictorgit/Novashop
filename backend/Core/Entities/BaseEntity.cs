using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class BaseEntity
{
    [BsonId]  // Indicates that this is the document identifier in MongoDB
    public ObjectId Id { get; set; }
}
