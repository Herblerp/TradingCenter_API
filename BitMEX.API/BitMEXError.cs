using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMEX_API
{
    class BitMEXError
    {
        public string message { get; set; }
        public string name { get; set; }

        public BitMEXError(string message, string name)
        {
            this.message = message;
            this.name = name;
        }
    }
}
