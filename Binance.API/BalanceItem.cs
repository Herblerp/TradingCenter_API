using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Binance_API
{
    public class BalanceItem
    {
        [JsonProperty(PropertyName = "asset")]
        public string Asset { set; get; }

        [JsonProperty(PropertyName = "free")]
        public decimal Free { set; get; }
        [JsonProperty(PropertyName = "locked")]
        public decimal Locked { set; get; }
    }
}
