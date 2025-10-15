
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public class LocalDbContext : DbContext
    {

        #region Commands
        #endregion

        #region Constructor

        public LocalDbContext()
        {

        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuiler)
        {
            // Startproject naam ophalen
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string entryAssemblyName = entryAssembly?.GetName().Name;
            // Lokaal path maken
            DatabasePath = @"C:\JOLT\" + entryAssemblyName + @"\";

            // Ensure the directory exists otherwise make it
            string directory = Path.GetDirectoryName(DatabasePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            DatabasePath = DatabasePath + "JOLTLocalDb.db";

            optionsBuiler.UseSqlite("Data Source=" + DatabasePath);

        }

        #endregion

        #region Properties

        // lijst van alle tabellen
        public DbSet<VideoInfoModel> InfoVideosDbSet { get; set; }
        public DbSet<MessageModel> MessagesDbSet { get; set; }
        public DbSet<MessageActiveModel> MessagesActiveDbSet { get; set; }
        public DbSet<MessageHistoryLineModel> MessagesHistoryDbSet { get; set; }
        public DbSet<MessageStepModel> MessageStepsDbSet { get; set; }
        public DbSet<SettingModel> SettingsDbSet { get; set; }
        public DbSet<ProductDetailModel> ProductDetailsDbSet { get; set; }
        public DbSet<SettingHistoryLineModel> SettingsHistoryDbSet { get; set; }
        public DbSet<HardwareModel> HardwareDbSet { get; set; }
        public DbSet<HardwareTypesModel> HardwareTypesDbSet { get; set; }

        public string DatabasePath;

        #endregion










    }
}
