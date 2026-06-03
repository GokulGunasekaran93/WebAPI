using ManagementService.Model;
using MongoDB.Driver;

namespace ManagementService.DataAccess.DatabaseContext;

public interface IDatabaseContext
{
    // only get should use
    IMongoCollection<User> user { get;}
    
    IMongoCollection<UserRole> userRole { get;}
}