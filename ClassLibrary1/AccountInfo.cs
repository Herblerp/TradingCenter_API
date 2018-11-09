using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance_API
{
    public class AccountInfo
    {
        public int MarkerCommission { get; set; }
        public int takerCommission { get; set; }
        public int buyerCommission { get; set; }
        public int SellerComission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public long Updatetime { get; set; }
        public List<BalanceItem> Balances { get; set; }
    }
}
