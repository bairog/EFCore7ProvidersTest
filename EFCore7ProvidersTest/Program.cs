using EFCore7ProvidersTest.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore7ProvidersTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var contextSQLite = new MyDBContext(true, "test"))
            using (var contextPostgres = new MyDBContext(false, "test"))
            {
                var migrations = contextSQLite.Database.GetPendingMigrations();
                if (migrations.Any())
                {
                    var migrator = contextSQLite.Database.GetService<IMigrator>();
                    foreach (var migration in migrations)
                        migrator.Migrate(migration);
                }
              
                using (var connection = contextSQLite.Database.GetDbConnection())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"ATTACH DATABASE 'D:\\test_text.db' AS test_text";
                    command.ExecuteNonQuery();
                }

                if (!contextSQLite.StudyYears.Any())
                {
                    var add = new StudyYear() { Year = 2022, Description = "Год 2022" };
                    contextSQLite.StudyYears.Add(add);
                    contextSQLite.SaveChanges();
                }
            }
        }
    }
}