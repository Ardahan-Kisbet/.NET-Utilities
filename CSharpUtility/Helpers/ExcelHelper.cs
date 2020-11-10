using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace CSharpUtility.Helpers
{
    class ExcelHelper
    {
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbooks xlWorkBooks = null;
        Excel.Workbook xlWorkBook = null;
        Excel.Sheets xlWorkSheets = null;
        Excel.Worksheet xlWorkSheet = null;
        Excel.Range xlRange = null;

        /// <summary>
        /// Disposes all excel com interfaces/references
        /// </summary>
        public void Dispose()
        {
            xlWorkBooks.Close();

            if (xlRange != null)
            {
                Marshal.ReleaseComObject(xlRange);
                xlRange = null;
            }

            if (xlWorkSheet != null)
            {
                Marshal.ReleaseComObject(xlWorkSheet);
                xlWorkSheet = null;
            }

            if (xlWorkSheets != null)
            {
                Marshal.ReleaseComObject(xlWorkSheets);
                xlWorkSheets = null;
            }

            if (xlWorkBook != null)
            {
                Marshal.ReleaseComObject(xlWorkBook);
                xlWorkBook = null;
            }

            if (xlWorkBooks != null)
            {
                Marshal.ReleaseComObject(xlWorkBooks);
                xlWorkBooks = null;
            }

            if (xlApp != null)
            {
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Exports given list to excel
        /// </summary>
        /// <param name="list"></param>
        /// <returns>returns byte array of temporarily created excel file</returns>
        internal byte[] ExportList(List<string> list)
        {
            byte[] result = null;
            try
            {
                if (xlApp != null)
                {
                    #region Create Excel on Server
                    object misValue = System.Reflection.Missing.Value;
                    // We have to keep each excel elements' references individually, otherwise we cannot be able to dispose (release resources) of sub elements
                    // Hence this usage should be prevented 
                    // xlWorkBook = xlApp.Workbooks.Add(misValue)
                    // WorkBooks reference should be kept as seperate object. Otherwise it will add WorkBooks to RCW and we will not have its reference in a variable
                    // Explanations in here --> https://stackoverflow.com/questions/17367411/cannot-close-excel-exe-after-interop-process/17367570#17367570
                    xlWorkBooks = xlApp.Workbooks;
                    xlWorkBook = xlWorkBooks.Add(misValue);
                    xlWorkSheets = xlWorkBook.Worksheets;
                    xlWorkSheet = (Excel.Worksheet)xlWorkSheets.get_Item(1);

                    // Load my data in here
                    xlRange = xlWorkSheet.Cells;
                    for (int i = 0; i < list.Count; i++)
                    {
                        xlRange[i + 1, 1] = list[i];
                    }

                    string tempPath = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + "_temp";
                    xlWorkBook.SaveAs(tempPath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    tempPath = xlWorkBook.FullName;
                    xlWorkBook.Close(true, misValue, misValue);
                    
                    // we have byte array. Send it as response to where neeeded
                    result = System.IO.File.ReadAllBytes(tempPath);
                    
                    // Do not forget to delete temporary excel file created on server file system
                    System.IO.File.Delete(tempPath);
                    #endregion
                }
                // Make sure you have installed Excel on your environment.
            }
            catch (Exception e)
            {
                string er = e.Message;
                result = System.Text.Encoding.ASCII.GetBytes(er);
            }

            finally
            {
                this.Dispose();
            }

            return result;
        }
    }
}
