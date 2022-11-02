using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameStuff.Models
{
    public class GameStuffContext : IdentityDbContext<User>
    {
        public GameStuffContext(DbContextOptions<GameStuffContext> options)
            : base(options)
        { }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // GameDeveloper: set primary key 
            modelBuilder.Entity<GameDeveloper>().HasKey(ba => new { GameId = ba.GameId, AuthorId = ba.DeveloperId });

            // GameDeveloper: set foreign keys 
            modelBuilder.Entity<GameDeveloper>().HasOne(ba => ba.Game)
                .WithMany(b => b.GameDevelopers)
                .HasForeignKey(ba => ba.GameId);
            modelBuilder.Entity<GameDeveloper>().HasOne(ba => ba.Developer)
                .WithMany(a => a.GameDevelopers)
                .HasForeignKey(ba => ba.DeveloperId);

            // Game: remove cascading delete with Genre
            modelBuilder.Entity<Game>().HasOne(b => b.Genre)
                .WithMany(g => g.Games)
                .OnDelete(DeleteBehavior.Restrict);

            // seed initial data
            modelBuilder.ApplyConfiguration(new SeedGenres());
            modelBuilder.ApplyConfiguration(new SeedGames());
            modelBuilder.ApplyConfiguration(new SeedDevelopers());
            modelBuilder.ApplyConfiguration(new SeedGameDeveloper());
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}