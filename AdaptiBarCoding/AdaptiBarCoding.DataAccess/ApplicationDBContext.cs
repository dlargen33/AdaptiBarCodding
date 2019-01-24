using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AdaptiBarCoding.Common.Configuration;
using AdaptiBarCoding.DataModel;

namespace AdaptiBarCoding.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        #region Private Readonly Fields

        private readonly AppSettings _appSettings;

        #endregion

        #region Constructors

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        public ApplicationDbContext(DbContextOptions options, IOptions<AppSettings> appSettings) : base(
            options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        #endregion

        #region Public Properties
        public DbSet<BlogPost> BlogPost { get; set; }

        #endregion

        #region Protected Members

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _appSettings.DBConnectionString;

                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
        #endregion
    }
}
