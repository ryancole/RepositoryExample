using System;

namespace RepositoryExample.Service
{
    public interface IWarcraftSession : IDisposable
    {
        #region Properties

        IAccountService Accounts { get; }
        ICharacterService Characters { get; }

        #endregion

        #region Methods

        bool SaveChanges();

        #endregion
    }
}