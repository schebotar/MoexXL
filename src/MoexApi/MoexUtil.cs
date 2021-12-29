using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MoexXL.MoexApi
{
    static class MoexUtil
    {
        public static async Task<object> GetTickerAttribute(string ticker, string attribute)
        {
            Dictionary<string, object> attributes;

            if (AddIn.memoryCache.Contains(ticker))
            {
                attributes = AddIn.memoryCache[ticker] as Dictionary<string, object>;
                Debug.WriteLine("Found in cache");
            }

            else
            {
                Debug.WriteLine("Not found in cache");
                string[] jsons = new[]
                {
                    await HttpUtil.GetStockJson(ticker),
                    await HttpUtil.GetBondJson(ticker)
                };

                attributes = jsons
                    .Select(json => JsonConvert.DeserializeObject<MoexIssResponse>(json))
                    .Select(response => response.GetAttributes())
                    .SelectMany(dict => dict)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);                   

                attributes.CacheWithKey(ticker);
            }

            object result = null;
            attributes.TryGetValue(attribute, out result);

            return result;
        }
    }
}
