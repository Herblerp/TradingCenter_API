using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int PortfolioId { get; set; }
        public int UserId { get; set; }

        public string Message { get; set; }
        public string PostedOn { get; set; }
    }
}
