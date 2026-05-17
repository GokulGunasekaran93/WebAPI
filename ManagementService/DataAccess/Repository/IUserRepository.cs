using ManagementService.Model;

namespace ManagementService.DataAccess.Repository;

public interface IUserRepository
{
    public List<User> GetUsers();
}