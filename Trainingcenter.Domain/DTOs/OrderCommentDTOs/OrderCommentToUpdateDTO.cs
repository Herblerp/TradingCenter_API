using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.OrderCommentDTOs
{
    public class OrderCommentToUpdateDTO
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
