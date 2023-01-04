using Microsoft.EntityFrameworkCore;
using ShoppingList.Models.Domain;

namespace ShoppingList.Data
{
    public class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options) : base(options)
        {

        }

        public DbSet<Goods> Goods { get; set; }
    }
}
