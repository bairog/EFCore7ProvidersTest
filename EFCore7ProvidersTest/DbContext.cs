using EFCore7ProvidersTest.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EFCore7ProvidersTest
{
    public class MyDBContext : DbContext
    {
        private Boolean IsSQLite;
        private String DatabaseName;


        //public static readonly ILoggerFactory DebugFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });

        public MyDBContext()
        {
            IsSQLite = true;
            DatabaseName = "test";
        }

        public MyDBContext(Boolean isSQLite, String databaseName)
        {
            IsSQLite = isSQLite;
            DatabaseName = databaseName;
        }

        public MyDBContext(DbContextOptions<MyDBContext> options, Boolean isSQLite, String databaseName)
            : base(options)
        {
            IsSQLite = isSQLite;
            DatabaseName = databaseName;
        }


        public DbSet<StudyYear> StudyYears { get; set; }
        public DbSet<StudyPeriod> StudyPeriods { get; set; }

        //настройка ограничений по классам через Code First Fluent API
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "";
            if (!optionsBuilder.IsConfigured)
                if (IsSQLite)
                {
                    connectionString = (new SqliteConnectionStringBuilder() { DataSource = $"D:\\{DatabaseName}.db" }).ToString();
                    optionsBuilder.UseSqlite(connectionString);
                }
                else
                {
                    connectionString = (new NpgsqlConnectionStringBuilder() { Host = "localhost", IntegratedSecurity = true }).ToString();
                    optionsBuilder.UseNpgsql(connectionString);
                }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (IsSQLite)
            {
                ////Добавление указаний что некоторые сущности нужно хранить в attach базах данных (сейчас их две:
                ////база с текстовыми полями (для локализации) и база с экземплярной информацией (рабочая база)
                //var sb = new SqliteConnectionStringBuilder(Database.GetConnectionString());
                //var attach = sb.DataSource.Split(';');
                ////база с текстовыми полями (именно она в будущем будет переводиться)
                //var typeTextDbSchemaName = Path.GetFileNameWithoutExtension(attach[0]);
                ////база с экзмплярной информацией (рабочая база)
                //var instanceDbSchemaName = Path.GetFileNameWithoutExtension(attach[1]);


                modelBuilder.Entity<StudyYear>(
                entityBuilder =>
                {
                    entityBuilder
                        .ToTable("StudyYears")
                        .SplitToTable(
                            "StudyYears",
                            "test_text",
                            tableBuilder =>
                            {
                                //tableBuilder.Property(se => se.Id).HasColumnName("CustomerId");
                                tableBuilder.Property(se => se.Description);
                            });
                });



            }

            //modelBuilder.Entity<StudyYear>().HasIndex(y => new { y.Year }).IsUnique();

            //modelBuilder.Entity<StudyPeriod>().HasIndex(sp => new { sp.StartDate, sp.EndDate }).IsUnique();
        }
    }
}
