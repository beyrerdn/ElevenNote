using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.ViewModels
{
    public class NoteListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }


    }

}
