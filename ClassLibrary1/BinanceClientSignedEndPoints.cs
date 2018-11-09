using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Binance_API
{
    public partial class BinanceClient
    {
        private void CheckAndAddReceiveWindow(long? recvWindow, IDictionary<string, string> parameters)
        {
            if (recvWindow.HasValue)
            {
                parameters.Add("recvWindow", recvWindow.Value.ToString(CultureInfo.InvariantCulture));
            }
        }
        public async Task<AccountInfo> GetAccountInfo(bool filterZeroBalances = false)
        {
            var response = await SendRequest<AccountInfo>("account", ApiVersion.Version3, ApiMethodType.Signed, HttpMethod.Get);
            if (filterZeroBalances)
            {
                response.Balances = response.Balances.Where(asset => asset.Free + asset.Locked != 0).ToList();
            }
            return response;
        }

        public async Task<IEnumerable<AllTradesInfo>> ListAllOrders(string symbol, long? orderId = null, int? limit = null, long? recvWindow = null)
        {
            var parameters = new Dictionary<string, string>
            {
                {"symbol", symbol},
            };

            CheckAndAddReceiveWindow(recvWindow, parameters);

            if (orderId.HasValue)
            {
                parameters.Add("orderId", orderId.Value.ToString(CultureInfo.InvariantCulture));
            }
            if (limit.HasValue)
            {
                parameters.Add("limit", limit.Value.ToString(CultureInfo.InvariantCulture));
            }
            return await SendRequest<List<AllTradesInfo>>("allOrders", ApiVersion.Version3, ApiMethodType.Signed, HttpMethod.Get, parameters);
        }
    }
}
