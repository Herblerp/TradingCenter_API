using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.NoteDTOs
{
    public class NoteToUpdateDTO
    {
        
        [Required]
        [MaxLength(500, ErrorMessage = "Message must be shorter than 500 characters")]
        public string message { get; set; }

        [Required]
        public int noteId { get; set; }
    }
}