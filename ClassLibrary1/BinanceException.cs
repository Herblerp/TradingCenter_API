using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance_API
{
    class BinanceException : Exception
    {

        public int ErrorCode { get; }
        public BinanceException(int errorCode, string message) : base(message)
        {
            errorCode = errorCode;
        }

        public override string ToString()
        {
            return $"Code: {ErrorCode} | Message: {Message}";
        }
    }
}
