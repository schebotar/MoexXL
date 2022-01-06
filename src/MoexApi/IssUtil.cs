using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcelDna.Integration;

namespace MoexXL.MoexApi
{
    static class IssUtil
    {
        public static async Task<object[,]> GetExcelRangeAsync(string ticker, string attribute, SecurityType securityType)
        {
            if (string.IsNullOrEmpty(ticker))
                return ExcelError.ExcelErrorValue.ToSingleExcelCell();

            ticker = ticker.ToUpper();
            string boardid = string.Empty;
            attribute = attribute.ToUpper();

            if (ticker.Contains(':'))
            {
                string[] split = ticker.Split(':');
                ticker = split[0];
                boardid = split[1];
            }

            SecuritiesResponse securitiesResponse = await GetSecuritiesResponse(ticker, securityType);
            Dictionary<string, Security> securities = securitiesResponse.GetSecurities();

            if (securities.Count == 0)
                return ExcelError.ExcelErrorGettingData.ToSingleExcelCell();

            else if (securities.Count > 1 && string.IsNullOrEmpty(boardid))
            {
                return new[]
                {
                    securities.Keys.Cast<object>().ToArray(),
                    securities.Values.Select(s => s.Attributes["BOARDNAME"]).ToArray()
                }
                .ToHeadedExcelRange("Идентификатор", "Режим торгов");
            }

            else
            {
                Security s;

                if (string.IsNullOrEmpty(boardid))
                    s = securities.First().Value;
                else
                {
                    if (securities.ContainsKey(boardid))
                        s = securities[boardid];
                    else return ExcelError.ExcelErrorValue.ToSingleExcelCell();
                }

                if (string.IsNullOrEmpty(attribute))
                {

                    MarketResponse marketResponse = await GetMarketResponse(securityType);
                    Dictionary<string, string> nameTitleDict = marketResponse.GetNameShortTitleDict();

                    var columns = new[]
                    {
                        s.Attributes.Keys.ToArray(),
                        s.Attributes.Keys.Select(k => nameTitleDict[k]).ToArray(),
                        s.Attributes.Values.ToArray()
                    };

                    return columns.ToHeadedExcelRange("Аттрибут", "Наименование", "Значение");
                }

                else return s.Attributes[attribute].ToSingleExcelCell();
            }
        }

        private static async Task<SecuritiesResponse> GetSecuritiesResponse(string ticker, SecurityType type)
        {
            if (ticker.TryGetFromCache(out SecuritiesResponse response))
            {
                return response;
            }

            else
            {
                string json = await HttpUtil.GetIssResponseAsync(type, ticker);
                response = JsonConvert.DeserializeObject<SecuritiesResponse>(json);
                response.CacheWithKey(ticker);

                return response;
            }
        }

        private static async Task<MarketResponse> GetMarketResponse(SecurityType type)
        {
            if (type.ToString().TryGetFromCache(out MarketResponse response))
            {
                return response;
            }

            else
            {
                string json = await HttpUtil.GetIssResponseAsync(type);
                response = JsonConvert.DeserializeObject<MarketResponse>(json);
                response.CacheWithKey(type.ToString());

                return response;
            }
        }
    }
}
