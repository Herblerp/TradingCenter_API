using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BitMEX_API
{
    public class OrderBookItem
    {
        public string Symbol { get; set; }
        public int Level { get; set; }
        public int BidSize { get; set; }
        public decimal BidPrice { get; set; }
        public int AskSize { get; set; }
        public decimal AskPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class BitMEXApi
    {
        private const string domain = "https://www.bitmex.com";
        private string apiKey;
        private string apiSecret;
        private int rateLimit;

        public BitMEXApi(string bitmexKey = "", string bitmexSecret = "", int rateLimit = 5000)
        {
            this.apiKey = bitmexKey;
            this.apiSecret = bitmexSecret;
            this.rateLimit = rateLimit;
        }

        private string BuildQueryData(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            StringBuilder b = new StringBuilder();
            foreach (var item in param)
                b.Append(string.Format("&{0}={1}", item.Key, WebUtility.UrlEncode(item.Value)));

            try { return b.ToString().Substring(1); }
            catch (Exception) { return ""; }
        }

        private string BuildJSON(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            var entries = new List<string>();
            foreach (var item in param)
                entries.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));

            return "{" + string.Join(",", entries) + "}";
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private long GetExpires()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600; // set expires one hour in the future
        }

        private string Query(string method, string function, Dictionary<string, string> param = null, bool auth = false, bool json = false)
        {
            string paramData = json ? BuildJSON(param) : BuildQueryData(param);
            string url = "/api/v1" + function + ((method == "GET" && paramData != "") ? "?" + paramData : "");
            string postData = (method != "GET") ? paramData : "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + url);
            webRequest.Method = method;

            if (auth)
            {
                string expires = GetExpires().ToString();
                string message = method + url + expires + postData;
                byte[] signatureBytes = hmacsha256(Encoding.UTF8.GetBytes(apiSecret), Encoding.UTF8.GetBytes(message));
                string signatureString = ByteArrayToString(signatureBytes);

                webRequest.Headers.Add("api-expires", expires);
                webRequest.Headers.Add("api-key", apiKey);
                webRequest.Headers.Add("api-signature", signatureString);
            }

            try
            {
                if (postData != "")
                {
                    webRequest.ContentType = json ? "application/json" : "application/x-www-form-urlencoded";
                    var data = Encoding.UTF8.GetBytes(postData);
                    using (var stream = webRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                using (WebResponse webResponse = webRequest.GetResponse())
                using (Stream str = webResponse.GetResponseStream())
                using (StreamReader sr = new StreamReader(str))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    if (response == null)
                        throw;

                    using (Stream str = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        //public List<OrderBookItem> GetOrderBook(string symbol, int depth)
        //{
        //    var param = new Dictionary<string, string>();
        //    param["symbol"] = symbol;
        //    param["depth"] = depth.ToString();
        //    string res = Query("GET", "/orderBook", param);
        //    return JsonSerializer.DeserializeFromString<List<OrderBookItem>>(res);
        //}
        public string GetPositions()
        {
            var param = new Dictionary<string, string>();
            param["reverse"] = true.ToString();
            return Query("GET", "/position", param, true);
        }

        //Returns all positions made after given DateTime, format is "2018-10-22 12:45:37.123"
        public string GetPositions(DateTime dt)
        {
            var param = new Dictionary<string, string>();
            param["reverse"] = true.ToString();
            param["startTime"] = dt.ToString();
            return Query("GET", "/position", param, true);
        }

        public string GetWallets()
        {
            var param = new Dictionary<string, string>();
            return Query("GET", "/user/wallet", param, true);
        }
        public string GetChat()
        {
            var param = new Dictionary<string, string>();
            param["count"] = 100.ToString();
            return Query("GET", "/chat", param, true);
        }

        public string GetWalletHistory()
        {
            var param = new Dictionary<string, string>();
            return Query("GET", "/user/walletHistory", param, true);
        }

        //Only returns the history of the wallet of a given currency
        public string GetWalletHistory(string currency)
        {
            var param = new Dictionary<string, string>();
            param["currency"] = currency;
            return Query("GET", "/user/walletHistory", param, true);
        }

        //Only returns a limited amount of data
        public string GetWalletHistory(int count)
        {
            var param = new Dictionary<string, string>();
            param["count"] = count.ToString();
            return Query("GET", "/user/walletHistory", param, true);
        }

        //Only returns a limited amount of data, of a given currency
        public string GetWalletHistory(string currency, int count)
        {
            var param = new Dictionary<string, string>();
            param["currency"] = currency;
            param["count"] = count.ToString();
            return Query("GET", "/user/walletHistory", param, true);
        }

        public string GetOrders()
        {
            var param = new Dictionary<string, string>();
            //param["symbol"] = "XBTUSD";
            //param["filter"] = "{\"open\":true}";
            //param["columns"] = "";
            //param["count"] = 100.ToString();
            //param["start"] = 0.ToString();
            param["reverse"] = true.ToString();
            //param["startTime"] = "";
            //param["endTime"] = "";
            return Query("GET", "/order", param, true);
        }

        //Returns all orders made after given DateTime, format is "2018-10-22 12:45:37.123"
        public string GetOrders(string dt)
        {
            var param = new Dictionary<string, string>();
            param["reverse"] = true.ToString();
            param["startTime"] = dt;
            return Query("GET", "/order", param, true);
        }
        public string PostOrders()
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = "XBTUSD";
            param["side"] = "Buy";
            param["orderQty"] = "1";
            param["ordType"] = "Market";
            return Query("POST", "/order", param, true);
        }

        public string DeleteOrders()
        {
            var param = new Dictionary<string, string>();
            param["orderID"] = "de709f12-2f24-9a36-b047-ab0ff090f0bb";
            param["text"] = "cancel order by ID";
            return Query("DELETE", "/order", param, true, true);
        }

        private byte[] hmacsha256(byte[] keyByte, byte[] messageBytes)
        {
            using (var hash = new HMACSHA256(keyByte))
            {
                return hash.ComputeHash(messageBytes);
            }
        }

        #region RateLimiter

        private long lastTicks = 0;
        private object thisLock = new object();

        private void RateLimit()
        {
            lock (thisLock)
            {
                long elapsedTicks = DateTime.Now.Ticks - lastTicks;
                var timespan = new TimeSpan(elapsedTicks);
                if (timespan.TotalMilliseconds < rateLimit)
                    Thread.Sleep(rateLimit - (int)timespan.TotalMilliseconds);
                lastTicks = DateTime.Now.Ticks;
            }
        }

        #endregion RateLimiter
    }
}