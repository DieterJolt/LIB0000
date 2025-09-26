
using System.IO;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public class ServerDbContext(string databasePath) : DbContext
    {

        #region Commands
        #endregion

        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        private bool IsServerDatabaseAvailable(string databasePath)
        {
            try
            {
                // Probeer een verbinding met de SQL Server-database te maken
                var connection = new SqlConnection(databasePath);
                connection.Open();
                connection.Close();
                return true; // De serverdatabase is beschikbaar
            }
            catch
            {
                MessageBox.Show("De serverdatabase is niet bereikbaar via " + databasePath.ToString());
                return false;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuiler)
        {
            // Startproject naam ophalen
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string entryAssemblyName = entryAssembly?.GetName().Name;

            databasePath = databasePath + " Database = " + entryAssemblyName + "ServerDb";

            if (!Local)
            {
                optionsBuiler.UseSqlServer(databasePath);
                Local = false;
            }
            else
            {

                // Lokaal path maken
                DatabaseLocalPath = @"C:\JOLT\" + entryAssemblyName + @"\";

                if (!Directory.Exists(DatabaseLocalPath))
                {
                    Directory.CreateDirectory(DatabaseLocalPath);
                }
                DatabaseLocalPath = DatabaseLocalPath + entryAssemblyName + "ServerDb.db";

                optionsBuiler.UseSqlite("Data Source=" + DatabaseLocalPath);

                Local = true;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hier nodige indexen definieren voor snellere query's

            //InstructionModel
            modelBuilder.Entity<InstructionModel>()
                .HasIndex(i => i.InstructionListVersionId)
                .HasDatabaseName("IX_InstructionListVersionId");

            modelBuilder.Entity<InstructionModel>()
                .HasIndex(i => i.Sequence)
                .HasDatabaseName("IX_Sequence");

            modelBuilder.Entity<InstructionModel>()
                .HasIndex(i => new { i.InstructionListVersionId, i.Sequence }) // Combined index for filtering & ordering
                .HasDatabaseName("IX_InstructionListVersionId_Sequence");

            modelBuilder.Entity<InstructionModel>()
                .HasIndex(i => i.InstructionType)
                .HasDatabaseName("IX_InstructionType");

            //InstructionTypesModel
            modelBuilder.Entity<InstructionTypesModel>()
                .HasIndex(t => t.InstructionType)
                .HasDatabaseName("IX_InstructionType_Join");
        }
        #endregion

        #region Properties

        // lijst van alle tabellen

        public static bool Local = false;

        public DbSet<OrderModel> OrderDbSet { get; set; }
        public DbSet<OrderHistoryModel> OrderHistoryDbSet { get; set; }
        public DbSet<RecipeModel> RecipeDbSet { get; set; }
        public DbSet<RecipeDetailModel> RecipeDetailsDbSet { get; set; }
        public DbSet<UserModel> UserDbSet { get; set; }
        public DbSet<UserHistoryModel> UserHistoryDbSet { get; set; }
        public DbSet<UserPagesModel> UserPagesDbSet { get; set; }
        public DbSet<InstructionModel> InstructionDbSet { get; set; }
        public DbSet<InstructionListModel> InstructionListDbSet { get; set; }
        public DbSet<InstructionListVersionModel> InstructionListVersionDbSet { get; set; }
        public DbSet<InstructionTypesModel> InstructionTypesDbSet { get; set; }
        public DbSet<InstructionHistoryModel> InstructionHistoryDbSet { get; set; }
        public DbSet<WorkstationModel> WorkstationDbSet { get; set; }
        public DbSet<ProductGroupModel> ProductGroupDbSet { get; set; }
        public DbSet<ProductModel> ProductDbSet { get; set; }

        public string DatabaseLocalPath;

        #endregion

    }
}
