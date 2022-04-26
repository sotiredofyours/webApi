using Meets.WebApi.Meetup;
using Microsoft.EntityFrameworkCore;

namespace Meets.WebApi;

public class DatabaseContext : DbContext
{
    public DbSet<MeetupEntity> Meetups { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
}