using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int OrderId { get; set; }

        public string Message { get; set; }
        
        public Order Order { get; set; }

    }
}
