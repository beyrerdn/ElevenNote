﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }



    }

}
