using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using NLog;

namespace Binance_API
{
    public partial class BinanceClient
    {
        private const string BaseUrl = "https://api.binance.com/api";
        private readonly string ApiKey;
        private readonly string SecretKey;
        //private readonly Logger logger = LogManager.GetCurrentClassLogger();

        
        public BinanceClient(string binanceKey = "", string binanceSecret = "")
        {
            this.ApiKey = binanceKey;
            this.SecretKey = binanceSecret;
        }

        private enum ApiMethodType
        {
            None = 0,
            ApiKey = 1,
            Signed = 2
        }

        private delegate T ResponseParseHandler<T>(string input);

        private static string GetParameterText(Dictionary<string, string> parameters)
        {
            if (parameters.Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (var item in parameters)
            {
                builder.Append($"&{item.Key}={item.Value}");
            }
            return builder.Remove(0, 1).ToString();
        }

        private async Task<T> SendRequest<T>(string methodName, string version, ApiMethodType apiMethod, HttpMethod httpMethod, Dictionary<string, string> parameters = null, ResponseParseHandler<T> customHandler = null)
        {
            if ((apiMethod == ApiMethodType.ApiKey && string.IsNullOrEmpty(ApiKey)) || (apiMethod == ApiMethodType.Signed && (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(SecretKey))))
            {
                throw new BinanceException(0, "You have to use correct API Key and Secret Key.");
            }

            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            if (apiMethod == ApiMethodType.Signed)
            {
                var timestamp = Utilities.GetCurrentMilliseconds();
                parameters.Add("timestamp", timestamp.ToString(CultureInfo.InvariantCulture));

                var parameterTextForSignature = GetParameterText(parameters);
                var SignedBytes = Utilities.Sign(SecretKey, parameterTextForSignature);
                parameters.Add("signature", Utilities.GetHexString(SignedBytes));
            }

            var parameterTextPrefix = parameters.Count > 0 ? "?" : string.Empty;
            var parameterText = GetParameterText(parameters);

            string response;

            using (var client = new WebClient())
            {
                if (apiMethod == ApiMethodType.Signed || apiMethod == ApiMethodType.ApiKey)
                {
                    client.Headers.Add("X-MBX-APIKEY", ApiKey);
                }

                try
                {
                    var getRequestUrl = $"{BaseUrl}/{version}/{methodName}/{parameterTextPrefix}/{parameterText}";
                    var postRequestUrl = $"{BaseUrl}/{version}/{methodName}";

                    response = httpMethod == HttpMethod.Get ?
                        await client.DownloadStringTaskAsync(getRequestUrl) :
                        await client.UploadStringTaskAsync(postRequestUrl, httpMethod.Method, parameterText);
                }

                catch (WebException webException)
                {
                    using (var reader = new StreamReader(webException.Response.GetResponseStream(), Encoding.UTF8))
                    {
                        var errorObject = JObject.Parse(reader.ReadToEnd());
                        var errorCode = (int)errorObject["code"];
                        var errorMessage = (string)errorObject["msg"];
                        throw new BinanceException(errorCode, errorMessage);
                    }
                }
            }
            return customHandler != null ? customHandler(response) : JsonConvert.DeserializeObject<T>(response);
        }
    }
}
