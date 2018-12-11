using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class OrderComment
    {
        public int OrderCommentId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime PostedOn { get; set; }

        public Order Order { get; set; }
        public User User { get; set; }

    }
}