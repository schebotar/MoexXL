using System;
using System.Collections.Generic;
using System.Linq;

namespace MoexXL.MoexApi
{
    internal class SecuritiesResponse : IssResponse
    {
        private Dictionary<string, Security> securities;

        public Dictionary<string, Security> GetSecurities()
        {
            if (securities != null)
                return securities;

            else
            {
                securities = new Dictionary<string, Security>();

                DataTable[] tables = new[]
                {
                    Securities,
                    Marketdata,
                    MarketdataYields
                };

                foreach (var t in tables)
                {
                    int boardidColumn = t.Columns.IndexOf("BOARDID");

                    foreach (var row in t.Rows)
                    {
                        Security s;
                        string boardid = row[boardidColumn] as string;

                        if (securities.ContainsKey(boardid))
                            s = securities[boardid];

                        else
                        {
                            securities.Add(boardid, new Security());
                            s = securities[boardid];
                        }

                        for (int column = 0; column < row.Count; column ++)
                        {
                            s.AddAttribute(t.Columns[column], row[column]);
                        }
                    }
                }

                return securities;
            }
        }
    }
}
