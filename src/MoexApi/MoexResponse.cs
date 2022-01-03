using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    public class MoexResponse
    {
        [JsonProperty("securities")]
        public readonly DataTable Securities;

        [JsonProperty("marketdata")]
        public readonly DataTable Marketdata;

        [JsonProperty("marketdata_yields")]
        public readonly DataTable MarketdataYields;

        private Dictionary<string, object>[] _attributes;

        public Dictionary<string, object>[] GetAttributes()
        {
            if (_attributes != null)
                return _attributes;

            else
            {
                int securitiesCount = Securities.DataRows.Count;
                _attributes = new Dictionary<string, object>[securitiesCount];

                DataTable[] tables = new[]
                {
                    Securities,
                    Marketdata,
                    MarketdataYields
                };

                for (int i = 0; i < securitiesCount; i++)
                {
                    _attributes[i] = new Dictionary<string, object>();

                    foreach (DataTable t in tables)
                    {
                        if (t.DataRows.Count == 0)
                            continue;

                        for (int column = 0; column < t.Columns.Count; column++)
                        {
                            if (_attributes[i].ContainsKey(t.Columns[column]))
                                continue;
                            else
                            {
                                object data = t.DataRows[i][column];

                                if (data != null && data.GetType() == typeof(string))
                                {
                                    DateTime dt;
                                    if (DateTime.TryParse((string)data, out dt))
                                        data = dt;
                                }

                                _attributes[i].Add(t.Columns[column], data);
                            }
                                
                        }
                    }
                }

                return _attributes;
            }
        }
    }

    public class DataTable
    {
        [JsonProperty("columns")]
        public List<string> Columns { get; set; }

        [JsonProperty("data")]
        public List<List<object>> DataRows { get; set; }
    }
}
