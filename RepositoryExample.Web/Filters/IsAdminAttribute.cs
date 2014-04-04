using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RepositoryExample.Entity;
using RepositoryExample.Service;

namespace RepositoryExample.Web.Filters
{
    public class IsAdminFilter : IActionFilter
    {
        private readonly IWarcraftSession m_session;

        public IsAdminFilter(IWarcraftSession session)
        {
            m_session = session;
        }

        #region Methods

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity;

            // default admin flag to false
            filterContext.Controller.ViewBag.isAdmin = false;

            if (identity.IsAuthenticated)
            {
                var account = m_session.Accounts.GetByEmail(identity.Name);

                // set the admin flag to true if needed
                if (account != null && account.IsAdmin)
                {
                    filterContext.Controller.ViewBag.isAdmin = true;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /* nothing */
        }

        #endregion
    }
}
