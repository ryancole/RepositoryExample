using System;

namespace RepositoryExample.Service
{
    public interface IWarcraftSession : IDisposable
    {
        #region Methods

        bool SaveChanges();

        #endregion

        #region Properties

        IAccountService Accounts { get; }

        ICharacterService Characters { get; }

        #endregion
    }
}