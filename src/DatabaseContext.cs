using Meets.WebApi.Meetup;
using Meets.WebApi.User;
using Microsoft.EntityFrameworkCore;

namespace Meets.WebApi;

public class DatabaseContext : DbContext
{
    public DbSet<MeetupEntity> Meetups { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
}