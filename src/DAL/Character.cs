using System;
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
            optionsBuilder.UseSqlite(Config["SQLite:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #if DEBUG
                modelBuilder.Entity<Model.Characters>().HasData(
                            new Model.Characters()
                            {
                                ID = Guid.NewGuid(),
                                SteamID = "76561198092541763",
                                Slot = 1,
                                Name = "Test",
                                Gender = 1,
                                Race = 1,
                                Kills = 0,
                                Gold = 100000,
                                Health = 1200,
                                Mana = 500,
                                Equipped = "{armor_paura=1}",
                                LeftHand = "smallarms_nh",
                                RightHand = "smallarms_nh",
                                BOH = "{smallarms_nh=2,armor_paura=1,crest_bou=2}"
                            },

                            new Model.Characters()
                            {
                                ID = Guid.NewGuid(),
                                SteamID = "76561198092543828",
                                Slot = 1,
                                Name = "Test2",
                                Gender = 0,
                                Race = 1,
                                Kills = 0,
                                Gold = 100000,
                                Health = 1200,
                                Mana = 500,
                                Equipped = "{armor_paura=1,crest_bou=1}",
                                BOH = "{smallarms_nh=2,armor_paura=1,crest_bou=2}"
                            }
                        );
            #endif
        }
    }
}
