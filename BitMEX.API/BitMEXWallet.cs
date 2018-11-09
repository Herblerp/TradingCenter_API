using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMEX_API
{
    class BitMEXWallet
    {
        public float? account { get; set; }
        public string currency { get; set; }
        public float? prevDeposited { get; set; }
        public float? prevWithdrawn { get; set; }
        public float? prevTransferIn { get; set; }
        public float? prevTransferOut { get; set; }
        public float? prevAmount { get; set; }
        public string prevTimestamp { get; set; }
        public float? deltaDeposited { get; set; }
        public float? deltaWithdrawn { get; set; }
        public float? deltaTransferIn { get; set; }
        public float? deltaTransferOut { get; set; }
        public float? deltaAmount { get; set; }
        public float? deposited { get; set; }
        public float? withdrawn { get; set; }
        public float? transferIn { get; set; }
        public float? transferOut { get; set; }
        public float? amount { get; set; }
        public float? pendingCredit { get; set; }
        public float? pendingDebit { get; set; }
        public float? confirmedDebit { get; set; }
        public string timestamp { get; set; }
        public string addr { get; set; }
        public string script { get; set; }
        public string[] withdrawalLock { get; set; }

        public BitMEXWallet(float? account, string currency, float? prevDeposited, float? prevWithdrawn, float? prevTransferIn, float? prevTransferOut, float? prevAmount, string prevTimestamp, float? deltaDeposited, float? deltaWithdrawn, float? deltaTransferIn, float? deltaTransferOut, float? deltaAmount, float? deposited, float? withdrawn, float? transferIn, float? transferOut, float? amount, float? pendingCredit, float? pendingDebit, float? confirmedDebit, string timestamp, string addr, string script, string[] withdrawalLock)
        {
            this.account = account;
            this.currency = currency;
            this.prevDeposited = prevDeposited;
            this.prevWithdrawn = prevWithdrawn;
            this.prevTransferIn = prevTransferIn;
            this.prevTransferOut = prevTransferOut;
            this.prevAmount = prevAmount;
            this.prevTimestamp = prevTimestamp;
            this.deltaDeposited = deltaDeposited;
            this.deltaWithdrawn = deltaWithdrawn;
            this.deltaTransferIn = deltaTransferIn;
            this.deltaTransferOut = deltaTransferOut;
            this.deltaAmount = deltaAmount;
            this.deposited = deposited;
            this.withdrawn = withdrawn;
            this.transferIn = transferIn;
            this.transferOut = transferOut;
            this.amount = amount;
            this.pendingCredit = pendingCredit;
            this.pendingDebit = pendingDebit;
            this.confirmedDebit = confirmedDebit;
            this.timestamp = timestamp;
            this.addr = addr;
            this.script = script;
            this.withdrawalLock = withdrawalLock;
        }
    }
}
