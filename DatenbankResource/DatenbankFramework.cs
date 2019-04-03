/************************************************************************************************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Dieser Gamemode wurde von Toby Gallenkamp Wohnhaft in der Fontanestraße 35 in Hatten programmiert.                                                                   @@
        @@ Die Entwicklung dieses Gamemodes wurde im Januar 2019 aufgenommen.                                                                                                   @@
        @@ Es dürfen nur von Toby Gallenkamp bestimmte Entwickler an diesem Gamemode arbeiten.                                                                                  @@
        @@ Alle Arbeiten an diesem Gamemode gehören automatisch Strawberry Roleplay und dürfen auch nur von Strawberry Roleplay genutzt werden.                                 @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Sollte dieser Gamemode in Hände dritter gelangen, so ist Toby Gallenkamp unter folgender Telefonnummer zu kontaktieren: 0160/1144521                                 @@
        @@ Sollte Toby Gallenkamp in diesem Fall nicht kontaktiert werden, so macht sich die Person nach § 106 Urheberrechtsgesetz strafbar.                                    @@
        @@ In einem solchen Fall wird nicht gezögert mit einem Anwalt gegen die Person vor zu gehen.                                                                            @@
        @@ Sollte Toby Gallenkamp durch einen Unfall oder sonstige Umstände sterben, so gehört dieser Gamemode Jakob Pritschmann wohnhaft in der Straße Hunteaue 1 in Hatten.   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
************************************************************************************************************************************************************************************************/

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
        public DbSet<JobLohn> srp_joblohn { get; set; }

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