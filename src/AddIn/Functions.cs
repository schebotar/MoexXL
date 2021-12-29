using ExcelDna.Integration;

namespace MoexXL
{
    public static class Functions
    {
        [ExcelFunction]
        public static object MOEXISS(string ticker, string field)
        {
            return ExcelAsyncUtil.Observe("MOEX", new object[] { ticker, field },
                () => new Observable(ticker, field));
        }
    }
}
