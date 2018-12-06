
using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.OrderDTO_s
{
    public class ProfitPerDayDTO
    {
        public string Name { get; set; }
        public int ExchangeKeyId { get; set; }
        public ICollection<ProfitPerDay> ProfitPerDayList { get; set; }

    }
}