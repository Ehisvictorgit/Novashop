using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class Product : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("quality")]
    public int Quality { get; set; }
    [BsonElement("expiration_date")]
    public DateTime ExpirationDate { get; set; }
    [BsonElement("price")]
    public int Price { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("currency")]
    public string Currency { get; set; }
    [BsonElement("category_id")]
    public ObjectId CategoryId { get; set; }
    [BsonElement("available")]
    public bool Available { get; set; }
    [BsonElement("image")]
    public string Image { get; set; }  // MongoDB mapea automáticamente este campo
}

