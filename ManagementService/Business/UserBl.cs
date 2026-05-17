using ManagementService.DataAccess.Repository;
using ManagementService.Model;

namespace ManagementService.Business;

public class UserBl : IUserBl
{
    private IUserRepository _users;
    public UserBl(IUserRepository users)
    {
        _users = users;
    }

    public List<User> GetUser()
    {
        return _users.GetUsers();
    }
}