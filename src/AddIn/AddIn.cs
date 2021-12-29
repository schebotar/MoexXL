using ExcelDna.Integration;
using ExcelDna.Registration;
using System.Net.Http;
using System.Runtime.Caching;

namespace MoexXL
{
    public class AddIn : IExcelAddIn
    {
        public static HttpClient httpClient;
        public static MemoryCache memoryCache;

        public void AutoClose()
        {
            memoryCache.Dispose();
        }

        public void AutoOpen()
        {
            httpClient = new HttpClient();
            memoryCache = new MemoryCache("MOEX");
            RegisterFunctions();
        }

        void RegisterFunctions()
        {
            ExcelRegistration.GetExcelFunctions()
                             .ProcessAsyncRegistrations(nativeAsyncIfAvailable: false)
                             .RegisterFunctions();
        }
    }
}
