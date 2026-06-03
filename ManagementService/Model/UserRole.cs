using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ManagementService.Model;

public class UserRole
{
    [BsonId]
    public ObjectId _id { get; set; } 
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}