using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.PortfolioDTO_s
{
    public class PortfolioToCreateDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name must be shorter than 50 characters")]
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "Description must be shorter than 500 characters")]
        public string Description { get; set; }
        [MaxLength(500, ErrorMessage = "Description must be shorter than 500 characters")]
        public string Goal { get; set; }
    }
}
