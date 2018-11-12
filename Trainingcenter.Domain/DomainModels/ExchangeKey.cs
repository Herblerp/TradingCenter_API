using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class ExchangeKey
    {
        public int ExchangeKeyId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public int LastId { get; set; }
        public DateTime LastRefresh { get; set; }

        public User User { get; set; }
    }
}
