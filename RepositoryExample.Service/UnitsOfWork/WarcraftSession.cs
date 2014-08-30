using RepositoryExample.Data;
using RepositoryExample.Entity;

namespace RepositoryExample.Service
{
    public class WarcraftSession : IWarcraftSession
    {
        private IAccountService m_accountService;
        private ICharacterService m_characterService;

        private readonly IWarcraftContext m_context;

        public WarcraftSession()
        {
            m_context = new WarcraftContext();
        }

        #region Methods

        public bool SaveChanges()
        {
            if (m_context.SaveChanges() > 0)
                return true;

            return false;
        }

        public void Dispose()
        {
            m_context.Dispose();
        }

        #endregion

        #region Properties

        public IAccountService Accounts
        {
            get
            {
                if (m_accountService == null)
                {
                    m_accountService = new AccountService(m_context);
                }

                return m_accountService;
            }
        }

        public ICharacterService Characters
        {
            get
            {
                if (m_characterService == null)
                {
                    m_characterService = new CharacterService(m_context);
                }

                return m_characterService;
            }
        }

        #endregion
    }
}