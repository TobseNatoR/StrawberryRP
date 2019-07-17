/************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Strawberry Roleplay Gamemode                                                                   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using Haupt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Datenbank
{
    public class DefaultDbContext : DbContext
    {
        //Basic Kontruktor
        public DefaultDbContext(DbContextOptions options) : base(options)
        {

        }

        //Hier werden die Klassen den Tabellen zugewiesen.
        public DbSet<Server> srp_server { get; set; }
        public DbSet<Whitelist> srp_whitelist { get; set; }
        public DbSet<Account> srp_accounts { get; set; }
        public DbSet<Auto> srp_fahrzeuge { get; set; }
        public DbSet<Immobilien> srp_immobilien { get; set; }
        public DbSet<Tankstelle> srp_tankstellen { get; set; }
        public DbSet<TankstellenPunkt> srp_tankstellenpunkte { get; set; }
        public DbSet<TankstellenInfo> srp_tankstelleninfo { get; set; }
        public DbSet<Supermarkt> srp_supermärkte { get; set; }
        public DbSet<Autohaus> srp_autohäuser { get; set; }
        public DbSet<Bot> srp_bots { get; set; }
        public DbSet<Log> srp_log { get; set; }
        public DbSet<Save> srp_saves { get; set; }
        public DbSet<Gruppen> srp_gruppen { get; set; }
        public DbSet<Bankautomaten> srp_bankautomaten { get; set; }
        public DbSet<Fraktionen> srp_fraktionen { get; set; }
        public DbSet<Fahrzeugvermietungen> srp_fahrzeugvermietungen { get; set; }
        public DbSet<RandomSpawns> srp_randomspawns { get; set; }

    }

    public class ContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        private static DefaultDbContext _instance;
        private static string _connectionString;

        public DefaultDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DefaultDbContext>();

            //Connection String das erste mal laden
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            //Connection einleiten
            builder.UseMySql(_connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DefaultDbContext).GetTypeInfo().Assembly.GetName().Name));

            return new DefaultDbContext(builder.Options);
        }

        public static DefaultDbContext Instance
        {
            get
            {
                if (_instance != null) return _instance;

                return _instance = new ContextFactory().CreateDbContext(new string[] { });
            }
            private set { }
        }

        private static void LoadConnectionString()
        {
            _connectionString = Haupt.GlobaleSachen.Verbindung;
        }

    }
}