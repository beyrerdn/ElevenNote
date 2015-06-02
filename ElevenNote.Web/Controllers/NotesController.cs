using ElevenNote.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    public class NotesController : Controller
    {
        [Authorize]
        // GET: Notes
        public ActionResult Index()
        {
            var notes = new List<NoteListViewModel>();
            notes.Add(new NoteListViewModel() {DateCreated = DateTime.Now, Name = "Some Title", Id = 1});
            notes.Add(new NoteListViewModel() { DateCreated = DateTime.Now, Name = "Some Title 2", Id = 2 });
            notes.Add(new NoteListViewModel() { DateCreated = DateTime.Now, Name = "Some Title 3", Id = 3 });

            return View(notes);
        }
    }
}