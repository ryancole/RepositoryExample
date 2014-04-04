using System;
using System.Data.Entity;

namespace RepositoryExample.Data
{
    public interface IWarcraftContext : IDisposable
    {
        #region Methods

        /// <summary>
        /// Commit pending changes
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Retrieve a DbSet for an entity
        /// </summary>
        IDbSet<T> GetDbSet<T>() where T : class;

        #endregion
    }
}