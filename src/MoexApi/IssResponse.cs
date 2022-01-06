using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    abstract class IssResponse 
    {
        [JsonProperty("securities")]
        public readonly DataTable Securities;

        [JsonProperty("marketdata")]
        public readonly DataTable Marketdata;

        [JsonProperty("marketdata_yields")]
        public readonly DataTable MarketdataYields;
    }

    public class DataTable
    {
        [JsonProperty("columns")]
        public List<string> Columns { get; set; }

        [JsonProperty("data")]
        public List<List<object>> Rows { get; set; }
    }
}
