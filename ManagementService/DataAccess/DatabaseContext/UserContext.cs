using ManagementService.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagementService.DataAccess.DatabaseContext;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public DbSet<User> users { get; set; } = null!;
}