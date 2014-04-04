using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Validation;
using RepositoryExample.Entity;
using RepositoryExample.Service;
using RepositoryExample.Web.Models;

namespace RepositoryExample.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWarcraftSession m_session;

        public AccountController(IWarcraftSession session)
        {
            m_session = session;
        }

        #region GET Handlers

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new AccountLoginViewModel();

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new AccountRegisterViewModel();

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            // clear the auth cookies
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        #endregion

        #region POST Handlers

        [HttpPost, AllowAnonymous]
        public ActionResult Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // fetch the requested account using the given email and password
            var account = m_session.Accounts.GetByEmailAndPassword(model.Email, model.Password);

            if (account == null)
            {
                ModelState.AddModelError("Email", "Login failed");
                return View(model);
            }

            // set auth cookie
            FormsAuthentication.SetAuthCookie(account.Email, true);

            return RedirectToAction("Index", "Character");
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Register(AccountRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // insert the desired account credentials
            var account = m_session.Accounts.Insert(new Account 
            {
                Email = model.Email,
                Password = model.Password,
                IsAdmin = true
            });

            if (account == null)
            {
                ModelState.AddModelError("Email", "Registration failed");
                return View(model);
            }

            try
            {
                // save the new account to the database
                if (m_session.SaveChanges())
                {
                    // set auth cookie
                    FormsAuthentication.SetAuthCookie(account.Email, true);

                    return RedirectToAction("Index", "Character");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entity in ex.EntityValidationErrors)
                {
                    foreach (var error in entity.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            return View(model);
        }

        #endregion
    }
}
