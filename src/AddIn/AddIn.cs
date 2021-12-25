using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;

namespace MoexXL
{
    public class AddIn : IExcelAddIn
    {
        public void AutoClose()
        {
            throw new NotImplementedException();
        }

        public void AutoOpen()
        {
            throw new NotImplementedException();
        }
    }
}
