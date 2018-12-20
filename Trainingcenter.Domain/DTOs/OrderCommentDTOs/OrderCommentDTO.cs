using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.OrderCommentDTOs
{
    public class OrderCommentDTO
    {
        public int CommentId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public string Message { get; set; }
        public string PostedOn { get; set; }
    }
}
