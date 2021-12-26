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

        public async static Task<string> GetStocks() =>
            await GetJsonByUri((new Uri(@"https://iss.moex.com/iss/engines/stock/markets/shares/securities.json?iss.meta=off&iss.json=extended")));
    }
}
