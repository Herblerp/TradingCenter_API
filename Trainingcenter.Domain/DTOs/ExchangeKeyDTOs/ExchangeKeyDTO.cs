using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.ExchangeKeyDTOs
{
    public class ExchangeKeyDTO
    {
        public int ExchangeKeyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Secret { get; set; }
    }
}
