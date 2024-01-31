using Microsoft.EntityFrameworkCore;
using PlacesWebApp.Models;

namespace PlacesWebApp.Data
{
    public class PlacesDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<PlaceItem> PlacesTable { get; set; }
    }
}
