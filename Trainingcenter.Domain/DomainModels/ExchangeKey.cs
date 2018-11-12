using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class ExchangeKey
    {
        public int ExchangeKeyId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Secret { get; set; }
        public int LastId { get; set; }
        public DateTime LastRefresh { get; set; }

        public User User { get; set; }
    }
}
