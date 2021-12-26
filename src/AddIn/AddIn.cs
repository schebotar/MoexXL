using ExcelDna.Integration;
using ExcelDna.Registration;
using System.Net.Http;

namespace MoexXL
{
    public class AddIn : IExcelAddIn
    {
        public static HttpClient httpClient;

        public void AutoClose()
        {

        }

        public void AutoOpen()
        {
            httpClient = new HttpClient();
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
