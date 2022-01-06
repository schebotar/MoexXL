using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoexXL
{
    static class HttpUtil
    {
        private static readonly HttpClient _httpClient = AddIn.httpClient;  

        public async static Task<string> GetIssResponseAsync(SecurityType securityType, string ticker = null)
        {
            Uri uri;

            if (string.IsNullOrEmpty(ticker))
            {
                uri = securityType.ToMarketsUri();
            }

            else
            {
                uri = ticker.ToSecuritiesUri(securityType);
            }

            return await _httpClient.GetStringAsync(uri);
        }

        private static Uri ToSecuritiesUri(this string ticker, SecurityType type)
        {
            UriBuilder ub = new UriBuilder("https", "iss.moex.com")
            {
                Path = $"iss/engines/stock/markets/{type}/securities/{ticker}.json"
            };

            return ub.Uri;
        }

        private static Uri ToMarketsUri(this SecurityType type)
        {
            UriBuilder ub = new UriBuilder("https", "iss.moex.com")
            {
                Path = $"iss/engines/stock/markets/{type}.json"
            };

            return ub.Uri;
        }
    }
}