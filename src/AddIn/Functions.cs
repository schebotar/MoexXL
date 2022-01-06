using ExcelDna.Integration;

namespace MoexXL
{
    public static class Functions
    {
        [ExcelFunction]
        public static object MOEXSTOCK(string ticker, string field)
        {
            return ExcelAsyncUtil.Observe("STOCK", new object[] { ticker, field },
                () => new Observable(ticker, field, SecurityType.Shares));
        }

        [ExcelFunction]
        public static object MOEXBOND(string ticker, string field)
        {
            return ExcelAsyncUtil.Observe("BOND", new object[] { ticker, field },
                () => new Observable(ticker, field, SecurityType.Bonds));
        }
    }
}
