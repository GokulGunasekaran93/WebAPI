using ManagementService.DataAccess.DatabaseContext;
using ManagementService.Model;
using MongoDB.Driver;

namespace ManagementService.DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDatabaseContext _db;

    public UserRepository(IDatabaseContext cntxt)
    {
        _db = cntxt;
    }
    
    public List<User> GetUsers()
    {
        
        var usrResult = _db.user.Find(_ => true).ToList();
        var usrRole = _db.userRole.Find(_ => true).ToList();

        foreach (var usr in usrResult)
        {
            usr.UserRoleName = usrRole.Find(a=>a.id == usr.userRole).name.ToString();
        }
        
        return usrResult;
        
        // var test = _db.user.AsQueryable()
        //     .Join(_db.userRole, user => user.userRole Equals(_db.userRole) )
        //    
        return new List<User>();
        //return joinedUsers.ToList();


        // return new List<d>(_users.Find(_ => true).ToList()); 
        // var collection = _users.Aggregate()
        //     .Lookup<User, UserRole>(
        //         ).ToList();
        // link using userRole (user) and id (userrole) collection 

        //  return _users.Find(_ => true).ToList();
    }
}