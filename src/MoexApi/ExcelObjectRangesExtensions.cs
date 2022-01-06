using System.Linq;
using System;

namespace MoexXL.MoexApi
{
    internal static class ExcelObjectRangesExtensions
    {
        public static object[,] ToHeadedExcelRange(this object[][] columns, params string[] headers)
        {
            if (columns.Length != headers.Length)
                throw new ArgumentException("Wrong headers count");

            if (columns.Select(a => a.Length).Distinct().Count() != 1)
                throw new ArgumentException("Rows count in columns are not the same");

            object[,] result = new object[columns[0].Length + 1, columns.Length];

            for (int column = 0; column < columns.Length; column++)
            {
                for (int row = 0; row < columns[0].Length + 1; row++)
                {
                    if (row == 0)
                        result[row, column] = headers[column];
                    else
                        result[row, column] = columns[column][row - 1];
                }
            }

            return result;
        }

        public static object[,] ToSingleExcelCell(this object o)
        {
            object[,] result = new object[1, 1];
            result[0, 0] = o;
            return result;
        }
    }
}
