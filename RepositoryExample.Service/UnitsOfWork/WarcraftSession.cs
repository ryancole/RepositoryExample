using RepositoryExample.Data;
using RepositoryExample.Entity;
using RepositoryExample.Repository;

namespace RepositoryExample.Service
{
    public class WarcraftSession : IWarcraftSession
    {
        private IWarcraftContext m_context;
        private IAccountService m_accountService;
        private ICharacterService m_characterService;

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
                    m_accountService = new AccountService(new Repository<Account>(m_context));

                return m_accountService;
            }
        }

        public ICharacterService Characters
        {
            get
            {
                if (m_characterService == null)
                    m_characterService = new CharacterService(new Repository<Character>(m_context));

                return m_characterService;
            }
        }

        #endregion
    }
}