using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    internal class MarketResponse : IssResponse
    {
        private Dictionary<string, string> nameTitle;

        public Dictionary<string, string> GetNameShortTitleDict()
        {
            if (nameTitle != null)
                return nameTitle;

            else
            {
                nameTitle = new Dictionary<string, string>();

                DataTable[] tables = new[]
                {
                    Securities,
                    Marketdata,
                    MarketdataYields
                };

                foreach (DataTable t in tables)
                {
                    int nameColumn = t.Columns.IndexOf("name");
                    int shortTitleColumn = t.Columns.IndexOf("short_title");

                    foreach (var row in t.Rows)
                    {
                        if (nameTitle.ContainsKey(row[nameColumn] as string))
                            continue;
                        else
                            nameTitle.Add(row[nameColumn] as string, row[shortTitleColumn] as string);
                    }
                }

                return nameTitle;
            }
        }
    }
}
