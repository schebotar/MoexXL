using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    public static class Parser
    {
        public static IEnumerable<StocksResponse> ParseStocks(string response)
        {
            return JsonConvert.DeserializeObject<IEnumerable<StocksResponse>>(response);
        }
    }
}
