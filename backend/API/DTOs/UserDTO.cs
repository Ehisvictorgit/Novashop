using MongoDB.Bson;

namespace API.DTOs;

public class UserDTO
{
    public ObjectId Id { get; set; }            // Table: Users | Field: Id
    public string User { get; set; }            // Table: Users | Field: User
}
