using WritersPlatform.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WritersPlatform.DataAccess.Contexts;

public class UsersDbContext: IdentityDbContext<AppUser>
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }
}
