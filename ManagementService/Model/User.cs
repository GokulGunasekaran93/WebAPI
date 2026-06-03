using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ManagementService.Model;

public class User
{
    [BsonId]
    public ObjectId _id { get; set; } 
    public int id { get; set; }
    public string name { get; set; }
    public string company { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string address { get; set; }
    public string zip { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string phone { get; set; }
    public string photo { get; set; }
    public int userRole { get; set; } = 0;
    [BsonIgnore]
    public string UserRoleName { get; set; }
}