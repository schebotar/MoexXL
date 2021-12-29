using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    class MoexIssResponse
    {
        [JsonProperty]
        public DataTable Securities { get; private set; }
        [JsonProperty]
        public DataTable Marketdata { get; private set; }
        [JsonProperty("marketdata_yields")]
        public DataTable MarketdataYields { get; private set; }

        public Dictionary<string, object> GetAttributes()
        {
            var _attributes = new Dictionary<string, object>();

            var tables = new[]
            {
                Securities,
                Marketdata,
                MarketdataYields
            };

            foreach (DataTable table in tables)
            {
                for (int i = 0; i < table.Columns.Length; i++)
                {
                    if (_attributes.ContainsKey(table.Columns[i]))
                        continue;
                    else if (table.Data.Length > 0 && table.Data[0][i] != null)
                        _attributes.Add(table.Columns[i], table.Data[0][i]);
                }
            }

            return _attributes;
        }

    }

    public class DataTable
    {
        [JsonProperty]
        public string[] Columns { get; private set; }
        [JsonProperty]
        public object[][] Data { get; private set; }
    }
}
