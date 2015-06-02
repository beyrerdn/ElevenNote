using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.ViewModels
{
    public class NoteEditViewModel
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(8000)]
        public string Contents { get; set; }

    }
}
