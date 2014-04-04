using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RepositoryExample.Entity;
using RepositoryExample.Service;

namespace RepositoryExample.Web.Filters
{
    public class AuthorizeAdminFilter : IAuthorizationFilter
    {
        private readonly IWarcraftSession m_session;

        public AuthorizeAdminFilter(IWarcraftSession session)
        {
            m_session = session;
        }

        #region Methods

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity;

            if (identity.IsAuthenticated)
            {
                var account = m_session.Accounts.GetByEmail(identity.Name);

                if (account != null && account.IsAdmin)
                    return;
            }

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                Controller = "Character",
                Action = "Index",
                Area = ""
            }));
        }

        #endregion
    }

    public class AuthorizeAdminAttribute : FilterAttribute
    {
        
    }
}