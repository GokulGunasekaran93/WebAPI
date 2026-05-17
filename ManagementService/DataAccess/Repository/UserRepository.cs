using ManagementService.DataAccess.DatabaseContext;
using ManagementService.Model;
using MongoDB.Driver;

namespace ManagementService.DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IDatabaseContext cntxt)
    {
        _users = cntxt.user;
    }
    
    public List<User> GetUsers()
    {
        return _users.Find(_ => true).ToList();
    }
}