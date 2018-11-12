using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int OrderId { get; set; }

        [Required]
        public string Message { get; set; }
        
        public Order Order { get; set; }

    }
}
