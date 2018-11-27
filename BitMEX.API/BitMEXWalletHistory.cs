using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMEX_API
{
    class BitMEXWalletHistory
    {
        public string transactID { get; set; }
		public int account { get; set; }
        public string currency { get; set; }
		public string transactType { get; set; }
		public int amount { get; set; }
        public int fee { get; set; }
		public string transactStatus { get; set; }
		public string address { get; set; }
		public string tx { get; set; }
		public string text { get; set; }
        public string transactTime { get; set; }
		public string timestamp { get; set; }


        public BitMEXWalletHistory(string transactID, int account, string currency, string transactType, int amount, int fee, string transactStatus, string address, string tx, string text, string transactTime, string timestamp)
        {
            this.transactID = transactID;
            this.account = account;
            this.currency = currency;
            this.transactType = transactType;
            this.amount = amount;
            this.fee = fee;
            this.transactStatus = transactStatus;
            this.address = address;
            this.tx = tx;
            this.text = text;
            this.transactTime = transactTime;
            this.timestamp = timestamp;
        }
    }
}
