using ManagementService.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ManagementService.DataAccess.DatabaseContext;

public class DatabaseContext : IDatabaseContext
{
    private readonly IMongoDatabase _db;

    public DatabaseContext(IOptions<MongoDBSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _db = client.GetDatabase(options.Value.Database);
        
    }
    public IMongoCollection<User> user => _db.GetCollection<User>("users");
}