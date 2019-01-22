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
        // Base constructor
        public DefaultDbContext(DbContextOptions options) : base(options)
        {

        }

        // Account model class created somewhere else
        public DbSet<Account> srp_accounts { get; set; }
        public DbSet<Auto> srp_fahrzeuge { get; set; }
        public DbSet<Log> srp_log { get; set; }

    }

    public class ContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        private static DefaultDbContext _instance;
        private static string _connectionString;

        public DefaultDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DefaultDbContext>();

            // Load the connection string for the first time
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            // Use it to init the connection
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
            _connectionString = Funktionen.Verbindung;
        }

    }
}