using ElevenNote.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ElevenNote.Services;


namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {   
        //Properties

        /// <summary>
        /// Instance of our Notes Service
        /// </summary>
        readonly NoteService _service = new NoteService();

        /// <summary>
        /// Currently logged in user --> get their ID
        /// </summary>
        Guid CurrentUserId
        {
            get
            {
                return new Guid(User.Identity.GetUserId());
            }
        }
        // GET: Notes

        public ActionResult Index(string sort = "") //redefined to allow the the binding engine to define sort through the QueryString
        {
            var notes = _service.GetAllForUser(this.CurrentUserId);
            //var sort = Request.QueryString["sort"] ?? ""; //via QueryString

            switch (sort)
            {
                case "name" :
                    notes = notes.OrderBy(o => o.Name).ToList();
                    break;
                case "modified" :
                    notes = notes.OrderBy(o => o.DateModified).ToList();
                    break;
                case "created" :
                    notes = notes.OrderBy(o => o.DateCreated).ToList();
                    break;
                default:
                    notes = notes.OrderBy(o => o.DateCreated).ToList();
                    break;
            }
            return View(notes);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult CreateGet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [ActionName("Create")]
    
        public ActionResult CreatePost(NoteEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                _service.Create(model, this.CurrentUserId);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id = -1) //if id isn't provided (user trying to get to ~/Notes/Details), id will be set to -1
        {
            if (id == -1)
            {
                TempData.Add("ErrorMessage", "You did not specify a note.");
                return RedirectToAction("Index");
            }

            var note = _service.GetById(id, this.CurrentUserId);

            //Make sure note is not null
            if (note == null)
            {
                TempData.Add("ErrorMessage", "That note couldn't be found.");
                return RedirectToAction("Index");
            }

            return View(note);
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditGet(int id = -1) //if id isn't provided (user trying to get to ~/Notes/Edit), id will be set to -1
        {
            if (id == -1)
            {
                TempData.Add("ErrorMessage", "You did not specify a note.");
                return RedirectToAction("Index");
            }

            //Attempt to get the note we're editing
            var note = _service.GetById(id, this.CurrentUserId);

            //Make sure we got a note back
            if (note == null) //return HttpNotFound(); //Returns "404" error.
            {
                TempData.Add("ErrorMessage", "That note couldn't be found.");
                return RedirectToAction("Index");
            }

            //If all looks good, pass the note to the view
            return View(note);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPost(NoteEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(model, this.CurrentUserId);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            //Attempt to get the note we're editing
            var note = _service.GetById(id, this.CurrentUserId);

            //Make sure we got a note back
            if (note == null) return HttpNotFound(); //Returns "404" error.

            //If all looks good, pass the note to the view
            return View(note);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DeletePost(int id)
        {
            if (ModelState.IsValid)
            {
                _service.Delete(id, this.CurrentUserId);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Toggle")]
        public ActionResult Toggle(int id = -1) //if id isn't provided (user trying to get to ~/Notes/Edit), id will be set to -1
        {
            if (id == -1)
            {
                TempData.Add("ErrorMessage", "You did not specify a note.");
                return RedirectToAction("Index");
            }

            //Attempt to get the note we're editing
            var note = _service.GetById(id, this.CurrentUserId);

            //Make sure we got a note back
            if (note == null)
            {
                TempData.Add("ErrorMessage", "That note couldn't be found.");
                return RedirectToAction("Index");
            }

            //If all looks good, toggle the IsFavorite
            _service.ToggleFavorite(id, this.CurrentUserId);
            return RedirectToAction("Index");
        }
    }
}