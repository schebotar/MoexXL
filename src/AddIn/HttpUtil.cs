using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoexXL
{
    static class HttpUtil
    {
        private static HttpClient _httpClient = AddIn.httpClient;

        private async static Task<string> GetJsonByUri(Uri uri)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls;

            return await _httpClient.GetStringAsync(uri);
        }

        public async static Task<string> GetStockJson(string ticker)
        {
            Uri uri = ticker.ToUri(SecurityType.Shares);
            return await GetJsonByUri(uri);
        }

        public async static Task<string> GetBondJson(string ticker)
        {
            Uri uri = ticker.ToUri(SecurityType.Bonds);
            return await GetJsonByUri(uri);
        }

        private static Uri ToUri(this string ticker, SecurityType type)
        {
            UriBuilder ub = new UriBuilder("https", "iss.moex.com");
            ub.Path = $"iss/engines/stock/markets/{type}/securities/{ticker}.json";
            //ub.Query = "iss.json=extended&iss.meta=off";

            return ub.Uri;
        }
    }
}