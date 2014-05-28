using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using RepositoryExample.Entity;
using RepositoryExample.Service;
using RepositoryExample.Web.Models;

namespace RepositoryExample.Web.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IWarcraftSession m_session;

        public CharacterController(IWarcraftSession session)
        {
            m_session = session;
        }

        #region GET Handlers

        public ActionResult Index()
        {
            // get the active user's account
            var account = m_session.Accounts.GetByEmail(User.Identity.Name);

            if (account == null)
                return RedirectToAction("Logout", "Account");

            // initialize the view model, containing all characters
            var model = new CharacterIndexViewModel
            {
                Account = account
            };

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CharacterCreateViewModel();

            return View(model);
        }

        public ActionResult Detail(string id)
        {
            // initialize view model, contains character details
            var model = new CharacterDetailViewModel
            {
                Account = m_session.Accounts.GetByEmail(User.Identity.Name),
                Character = m_session.Characters.GetByName(id)
            };

            if (model.Character == null || model.Account == null)
                return RedirectToAction("Index");

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            // get active user's account
            var account = m_session.Accounts.GetByEmail(User.Identity.Name);

            if (account == null)
                return RedirectToAction("Logout", "Account");

            // get the requested character
            var character = m_session.Characters.GetByName(id);

            // make sure this character can and should be deleted
            if (character != null &&
                character.Active &&
                character.Account.AccountId == account.AccountId)
            {
                // disable the character
                character.Active = false;

                // save changes to the database
                m_session.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Undelete(string id)
        {
            // get active user's account
            var account = m_session.Accounts.GetByEmail(User.Identity.Name);

            if (account == null)
                return RedirectToAction("Logout", "Account");

            // get the requested character
            var character = m_session.Characters.GetByName(id);

            // make sure this character can and should be undeleted
            if (character != null &&
                character.Active == false &&
                character.Account.AccountId == account.AccountId)
            {
                // enable the character
                character.Active = true;

                // save changes to the database
                m_session.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region POST Handlers

        [HttpPost]
        public ActionResult Create(CharacterCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // get the current user's account
            var account = m_session.Accounts.GetByEmail(User.Identity.Name);

            if (account == null)
                return RedirectToAction("Logout", "Account");

            try
            {
                // insert the desired character details
                var character = m_session.Characters.Insert(new Character
                {
                    Name = model.Name,
                    Race = model.Race,
                    Class = model.Class,
                    Faction = model.Faction,
                    Account = account
                });

                // save the new character to the database
                if (m_session.SaveChanges())
                {
                    return RedirectToAction("Index");
                }
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(model);
        }

        #endregion
    }
}
