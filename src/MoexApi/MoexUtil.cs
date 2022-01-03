using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcelDna.Integration;

namespace MoexXL.MoexApi
{
    static class MoexUtil
    {
        public static async Task<object[,]> GetStockInfo(string ticker, string attribute)
        {
            object[,] result = null;
            string boardid = string.Empty;

            if (string.IsNullOrEmpty(ticker))
                return SingleCell(ExcelError.ExcelErrorValue);

            if (ticker.Contains(':'))
            {
                string[] split = ticker.Split(':');
                ticker = split[0];
                boardid = split[1];
            }

            MoexResponse response = await GetMoexSharesAsync(ticker);
            //MoexResponse response = new MoexResponse[] { shares, bonds }.Where(x => x.Securities.DataRows.Count > 0).FirstOrDefault();

            if (response == null)
                return SingleCell(ExcelError.ExcelErrorGettingData);

            if (string.IsNullOrEmpty(attribute))
            {
                var attributes = response.GetAttributes();

                if (attributes.Length == 0)
                    return SingleCell(ExcelError.ExcelErrorValue);

                else if (attributes.Length > 1 && string.IsNullOrEmpty(boardid))
                {
                    result = new object[attributes.Length + 1, 2];
                    result[0, 0] = "Идентификатор";
                    result[0, 1] = "Режим торгов";

                    for (int security = 0; security < attributes.Length; security++)
                    {
                        result[security + 1, 0] = attributes[security]["BOARDID"];
                        result[security + 1, 1] = attributes[security]["BOARDNAME"];
                    }
                }

                else
                {
                    Dictionary<string, object> attributesDict;

                    if (string.IsNullOrEmpty(boardid))
                        attributesDict = attributes[0];
                    else
                        attributesDict = attributes.Where(d => d["BOARDID"].Equals(boardid)).FirstOrDefault();

                    result = new object[attributesDict.Count + 1, 2];
                    result[0, 0] = "Аттрибут";
                    result[0, 1] = "Значение";

                    var kvp = attributesDict.ToArray();

                    for (int i = 0; i < kvp.Length; i++)
                    {
                        result[i + 1, 0] = kvp[i].Key;
                        result[i + 1, 1] = kvp[i].Value;
                    }
                }
            }

            else
            {
                var attributes = response.GetAttributes();
                Dictionary<string, object> attributesDict;

                if (string.IsNullOrEmpty(boardid))
                    attributesDict = attributes[0];
                else
                    attributesDict = attributes.Where(d => d["BOARDID"].Equals(boardid)).FirstOrDefault();

                if (attributesDict.ContainsKey(attribute))
                    result = SingleCell(attributesDict[attribute]);
                else
                    result = SingleCell(ExcelError.ExcelErrorValue);
            }

            return result;
        }

        private static object[,] SingleCell(object o)
        {
            object[,] result = new object[1, 1];
            result[0, 0] = o;
            return result;
        }

        private static async Task<MoexResponse> GetMoexSharesAsync(string ticker)
        {
            string json = await HttpUtil.GetStockJson(ticker);
            MoexResponse response = JsonConvert.DeserializeObject<MoexResponse>(json);

            return response;
        }

        private static async Task<MoexResponse> GetMoexBondsAsync(string ticker)
        {
            string json = await HttpUtil.GetBondJson(ticker);
            MoexResponse response = JsonConvert.DeserializeObject<MoexResponse>(json);

            return response;
        }
    }
}
