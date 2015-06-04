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
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(128, ErrorMessage = "Please enter a name with less than 128 characters.")]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(8000, ErrorMessage = "Please limit your note to 8,000 characters.")]
        public string Contents { get; set; }

    }
}
