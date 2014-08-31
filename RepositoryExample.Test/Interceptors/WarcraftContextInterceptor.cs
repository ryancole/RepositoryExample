using System;
using System.Diagnostics;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace RepositoryExample.Test
{
    public class WarcraftContextInterceptor : IDbConfigurationInterceptor
    {
        #region Methods

        public void Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbConfigurationInterceptionContext interceptionContext)
        {
            var formatterFactory = loadedEventArgs.DependencyResolver
                                                  .GetService<Func<DbContext, Action<string>, DatabaseLogFormatter>>();

            var formatter = formatterFactory(null, (s) => { Debug.WriteLine(s); });

            DbInterception.Add(formatter);
        }

        #endregion
    }
}
