using Meets.WebApi.Meetup;
using Microsoft.EntityFrameworkCore;

namespace Meets.WebApi;

internal class DatabaseContext : DbContext
{
    public DbSet<MeetupEntity> Meetups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseNpgsql("Server=localhost;Port=5432;Database=asp_courses;User Id =user;Password=user");
    
}