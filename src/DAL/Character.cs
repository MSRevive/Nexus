using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MSNexus.DAL
{
    public class Character : DbContext
    {
        protected IConfiguration Config { get; }
        public DbSet<Model.Characters> Characters { get; set; }

        public Character(IConfiguration configuration)
        {
            Config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Config.GetConnectionString("cs"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Characters>().HasData(
                new Model.Characters()
                {
                    ID = "STEAM_0-0-555",
                    Slot = 1,
                    Name = "Test",
                    Gender = "Male",
                    Race = "Human",
                    Kills = 0,
                    Gold = 100000,
                    Health = 1200,
                    Mana = 500,
                    Equipped = "smallarms_nh;armor_paura"
                },

                new Model.Characters()
                {
                    ID = "STEAM_0-0-5555",
                    Slot = 1,
                    Name = "Test2",
                    Gender = "Male",
                    Race = "Human",
                    Kills = 0,
                    Gold = 100000,
                    Health = 1200,
                    Mana = 500,
                    Equipped = "smallarms_nh;armor_paura"
                }
            );
        }
    }
}
