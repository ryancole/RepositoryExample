﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using RepositoryExample.Entity;
using RepositoryExample.Service;
using RepositoryExample.Web.Models;
using RepositoryExample.Web.Filters;
using RepositoryExample.Web.Utilities;

namespace RepositoryExample.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IWarcraftSession m_session;

        public AdministrationController(IWarcraftSession session)
        {
            m_session = session;
        }

        #region GET Handlers

        [AuthorizeAdmin]
        public ActionResult Index()
        {
            var model = new AdministrationIndexViewModel
            {
                Characters = m_session.Characters.GetAll()
            };

            return View(model);
        }

        [AuthorizeAdmin]
        public ActionResult Edit(string id)
        {
            var character = m_session.Characters.GetByName(id);

            if (character == null)
                return RedirectToAction("Index");

            var model = new AdministrationEditViewModel
            {
                Character = character,
                Level = character.Level
            };

            return View(model);
        }

        #endregion

        #region POST Handlers

        [HttpPost, AuthorizeAdmin]
        public ActionResult Edit(AdministrationEditViewModel model, string id)
        {
            var character = m_session.Characters.GetByName(id);

            if (character == null)
                return RedirectToAction("Index");

            // reset the character
            model.Character = character;

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // set the new character level
                character.Level = model.Level;

                // save changes to the database
                if (m_session.SaveChanges())
                {
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                ControllerUtilities.MergeValidationErrors(ModelState, ex);
            }

            return View(model);
        }

        #endregion
    }
}
