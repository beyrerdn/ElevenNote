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
        // GET: Notes
        public ActionResult Index()
        {
            //Currently logged in user --> get their ID
            var userId = new Guid(User.Identity.GetUserId()); //we can call this "GetUserId()" thanks to "using Microsoft.AspNet.Identity

            var service = new NoteService();

            return View(service.GetAllForUser(userId));
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
                var userId = new Guid(User.Identity.GetUserId());
                var service = new NoteService();
                service.Create(model, userId);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}