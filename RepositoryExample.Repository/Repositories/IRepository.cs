using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace RepositoryExample.Repository
{
    public interface IRepository<T> where T : class
    {
        #region Methods

        /// <summary>
        /// Insert an entity
        /// </summary>
        T Insert(T entity);

        /// <summary>
        /// count entities matching a predicate
        /// </summary>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieve entities matching a predicate
        /// </summary>
        ICollection<T> Where(Expression<Func<T, bool>> predicate);

        #endregion

        #region Properties

        /// <summary>
        /// Retrieve all entities
        /// </summary>
        IQueryable<T> All { get; }

        #endregion
    }
}