using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.OrderCommentDTOs
{
    public class OrderCommentToCreateDTO
    {
        public int UserId { get; set; }

        [Required]
        public int OrderId { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Max 200 char.")]
        public string Message { get; set; }
    }
}
