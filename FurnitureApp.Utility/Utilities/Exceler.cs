using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Utility
{
    public class Exceler
    {
        public static string GetCellValue(int row, int column, ref Microsoft.Office.Interop.Excel.Range xlCells, ref Microsoft.Office.Interop.Excel.Range xlRange)
        {
            xlRange = xlCells[row, column] as Microsoft.Office.Interop.Excel.Range;
            return $"{xlRange.Text}";
        }
        public static void SetValueToCell(int row, int column, string text, ref Microsoft.Office.Interop.Excel.Range xlCells, ref Microsoft.Office.Interop.Excel.Range xlRange)
        {
            xlRange = xlCells[row, column] as Microsoft.Office.Interop.Excel.Range;
            xlRange.Value = text;
        }
        public static void SetValueToCells(int row, int column, List<List<string>> lines, ref Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            Microsoft.Office.Interop.Excel.Range range1 = null;
            Microsoft.Office.Interop.Excel.Range range2 = null;

            try
            {
                if (lines.Count == 0) { return; }

                range1 = worksheet.Cells[row, column] as Microsoft.Office.Interop.Excel.Range;
                range2 = worksheet.Cells[row + lines.Count - 1, column + lines[0].Count - 1] as Microsoft.Office.Interop.Excel.Range;

                var temp = worksheet.Range[range1, range2];

                var arys = new object[lines.Count, lines[0].Count];

                foreach (var (fields, i) in lines.WithIndex())
                {
                    foreach (var (field, j) in fields.WithIndex())
                    {
                        arys[i, j] = field;
                    }
                }
                temp.Value = arys;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                if (range1 != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
                }
                if (range2 != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        public static void  ReleaseObjects(ref Microsoft.Office.Interop.Excel.Application xlApp,
                                    ref Microsoft.Office.Interop.Excel.Workbooks xlBooks,
                                    ref Microsoft.Office.Interop.Excel.Workbook xlBook,
                                    ref Microsoft.Office.Interop.Excel.Sheets xlSheets,
                                    ref Microsoft.Office.Interop.Excel.Worksheet xlSheet,
                                    ref Microsoft.Office.Interop.Excel.Range xlCells,
                                    ref Microsoft.Office.Interop.Excel.Range xlRange)
        {
            //if (xlBook != null) { xlBook.Close(); }
            //if (xlBooks != null) { xlBooks.Close(); }
            if (xlApp != null) { xlApp.Quit(); }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            if (xlApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            }

            if (xlBooks != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);
            }

            if (xlBook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
            }

            if (xlSheets != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheets);
            }

            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
            }

            if (xlCells != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCells);
            }

            if (xlRange != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRange);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
