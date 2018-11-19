using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.NoteDTOs
{
    public class NoteToCreateDTO
    {
        [Required]
        [MaxLength(500, ErrorMessage = "Message must be shorter than 500 characters")]
        public string Message { get; set; }

        [Required]
        public int PortfolioId { get; set; }
    }
}
