using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Collections.Generic;
using RepositoryExample.Data;

namespace RepositoryExample.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IWarcraftContext m_context;

        public Repository(IWarcraftContext context)
        {
            m_context = context;
        }

        #region Methods

        /// <summary>
        /// Insert an entity
        /// </summary>
        public T Insert(T entity)
        {
            var entities = m_context.GetDbSet<T>();

            return entities.Add(entity);
        }

        /// <summary>
        /// count entities matching a predicate
        /// </summary>
        public int Count(Expression<Func<T, bool>> predicate)
        {
            var entities = m_context.GetDbSet<T>();

            return entities.Count(predicate);
        }

        /// <summary>
        /// Retrieve entities matching a predicate
        /// </summary>
        public ICollection<T> Where(Expression<Func<T, bool>> predicate)
        {
            var entities = m_context.GetDbSet<T>();

            return entities.Where(predicate).ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Retrieve all entities
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                var entities = m_context.GetDbSet<T>();

                return entities.AsQueryable();
            }
        }

        #endregion
    }
}