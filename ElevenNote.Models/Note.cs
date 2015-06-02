using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Contents { get; set; }

        public Guid ApplicationUserId { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }



    }

}
