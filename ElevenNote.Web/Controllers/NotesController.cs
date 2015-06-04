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

        public ActionResult Index()
        {
            
            return View(_service.GetAllForUser(this.CurrentUserId));
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
        public ActionResult Details(int id)
        {
            var note = _service.GetById(id, this.CurrentUserId);
            if (note == null) return HttpNotFound(); //Returns "404" error.

            return View(note);
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditGet(int id)
        {
            //Attempt to get the note we're editing
            var note = _service.GetById(id, this.CurrentUserId);

            //Make sure we got a note back
            if (note == null) return HttpNotFound(); //Returns "404" error.

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
    }
}