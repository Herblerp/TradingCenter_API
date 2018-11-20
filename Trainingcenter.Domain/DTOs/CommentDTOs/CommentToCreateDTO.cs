using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.CommentDTOs
{
    public class CommentToCreateDTO
    {
        public int UserId { get; set; }

        [Required]
        public int PortfolioId { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage ="Max 200 char.")]
        public string Message { get; set; }
    }
}
