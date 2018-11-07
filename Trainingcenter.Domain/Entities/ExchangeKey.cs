using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.DL.Entities
{
    public class ExchangeKey
    {
        public int ExchangeKeyId { get; set; }
        public int UserId { get; set; }

        public string Key { get; set; }
        public string Secret { get; set; }

        public User User { get; set; }
    }
}
