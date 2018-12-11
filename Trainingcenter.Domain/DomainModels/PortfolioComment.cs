using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class PortfolioComment
    {
        public int PortfolioCommentId { get; set; }
        public int PortfolioId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime PostedOn { get; set; }
        
        public Portfolio Portfolio { get; set; }
        public User User { get; set; }

    }
}
