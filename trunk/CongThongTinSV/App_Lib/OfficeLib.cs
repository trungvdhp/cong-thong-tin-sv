using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.IO;

namespace CongThongTinSV.App_Lib
{
    public class ExcelExportor
    {
        private Excel.Application ExcelApp;
        private Excel.Workbook workbook;
        private Excel.Sheets sheets;
        private Excel.Worksheet worksheet;
        private Excel.Range range;

        /// <summary>
        /// File Name of the Export output file
        /// </summary>
        public string ExportFileName { get; set; }

        /// <summary>
        /// Template File Name - Using Template file to Export
        /// </summary>
        public string TemplateFileName { get; set; }

        /// <summary>
        /// Sheet name to Export the data
        /// </summary>
        public string ExportSheetName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ExportFileName"></param>
        /// <param name="TemplateFileName"></param>
        /// <param name="ExportSheetName"></param>
        public ExcelExportor(string ExportFileName, string TemplateFileName = "", string ExportSheetName = "Sheet1")
        {
            this.ExportFileName = ExportFileName;
            this.TemplateFileName = TemplateFileName;
            this.ExportSheetName = Utility.ValidateName(ExportSheetName, 31);

            // Create excel application
            ExcelApp = new Excel.Application();
            ExcelApp.Visible = false;
            ExcelApp.DisplayAlerts = false;

            // Is Export needs to be exported to a Template file
            if (TemplateFileName != "")
            {
                // Load the work book
                workbook = ExcelApp.Workbooks.Open(TemplateFileName, 0, false, 5, "", "", false,
                    Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                sheets = workbook.Sheets;
                worksheet = (Excel.Worksheet)sheets.get_Item(1); // To avoid unassigned variable error

                bool IsWorkSheetFound = false;

                //Check is there any worksheet with the name provided. If yes, clear all data inside to fill new data
                for (int intSheetIndex = 1; intSheetIndex <= sheets.Count; intSheetIndex++)
                {
                    worksheet = (Excel.Worksheet)sheets.get_Item(intSheetIndex);

                    if (worksheet.Name.ToString().Equals(ExportSheetName))
                    {
                        IsWorkSheetFound = true;
                        break;
                    }
                }

                // If No work sheet found, add it at the last
                if (!IsWorkSheetFound)
                {
                    worksheet = (Excel.Worksheet)workbook.Sheets.Add(
                        Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
                    worksheet.Name = ExportSheetName;
                }
            }
            else
            {
                // Adding new work book
                workbook = ExcelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                sheets = workbook.Sheets;
                worksheet = (Excel.Worksheet)sheets.get_Item(1);
                worksheet.Name = ExportSheetName;
            }
        }

        /// <summary>
        /// Set number format cell
        /// </summary>
        /// <param name="format">Default = @ or General</param>
        public void SetNumberFormat(string format = "@")
        {
            range.NumberFormat = format;
        }

        /// <summary>
        /// Set BackColor
        /// </summary>
        /// <param name="color">Default = White</param>
        public void SetBackColor(string color = "White")
        {
            range.Interior.Color = Color.FromName(color).ToArgb();
        }

        /// <summary>
        /// Set ForeColor
        /// </summary>
        /// <param name="color">Default = Black</param>
        public void SetForeColor(string color = "Black")
        {
            range.Font.Color = Color.FromName(color).ToArgb();
        }

        /// <summary>
        /// Set name of font
        /// </summary>
        /// <param name="fontName">Default is Arial</param>
        public void SetFontName(string fontName = "Arial")
        {
            range.Font.Name = fontName;
        }
        
        /// <summary>
        /// Set size of font
        /// </summary>
        /// <param name="size">Default = 10</param>
        public void SetFontSize(int size = 10)
        {
            range.Font.Size = size;
        }

        /// <summary>
        /// Set FontBold
        /// </summary>
        /// <param name="isBold">Default = true</param>
        public void SetFontBold(bool isBold = true)
        {
            range.Font.Bold = isBold;
        }

        /// <summary>
        /// Set Font Italic
        /// </summary>
        /// <param name="isBold">Default = true</param>
        public void SetFontItalic(bool isItalic = true)
        {
            range.Font.Italic = isItalic;
        }

        /// <summary>
        /// Set Font UnderLine
        /// </summary>
        /// <param name="isBold">Default = true</param>
        public void SetFontUnderLine(bool isUnderLine = true)
        {
            range.Font.Underline = isUnderLine;
        }

        /// <summary>
        /// Set horizontal alignment
        /// </summary>
        /// <param name="isBold">Default = Left</param>
        public void SetHorizontalAlignment(Excel.XlHAlign align = Excel.XlHAlign.xlHAlignLeft)
        {
            range.HorizontalAlignment = align;
        }

        /// <summary>
        /// Set vertical alignment
        /// </summary>
        /// <param name="isBold">Default = center</param>
        public void SetVerticalAlignment(Excel.XlHAlign align = Excel.XlHAlign.xlHAlignCenter)
        {
            range.VerticalAlignment = align;
        }

        /// <summary>
        /// Set width of column
        /// </summary>
        /// <param name="size">Default = -1 is auto fit, size = 2 = 15px</param>
        public void SetColumnWidth(int size = -1)
        {
            if (size == -1)
            {
                range.Columns.AutoFit();
            }
            else
            {
                range.ColumnWidth = size;
            }
        }

        /// <summary>
        /// Set height of row
        /// </summary>
        /// <param name="size">Default = -1 is auto fit, size = 3 = 4px</param>
        public void SetRowHeight(int size = -1)
        {
            if (size == -1)
            {
                range.Rows.AutoFit();
            }
            else
            {
                range.RowHeight = size;
            }
        }

        /// <summary>
        /// Merge column
        /// </summary>
        /// <param name="columnCount">number of columns for merge</param>
        public void MergeColumns(int columnCount = 0)
        {
            range.Merge(columnCount);
        }

        /// <summary>
        /// Set line style of boders
        /// </summary>
        /// <param name="borderLineStyles">Lines of [Top, Bottom, Left, Right, InsideHorizontal, InsideVertical]</param>
        public void SetBoderLineStyles(Excel.XlLineStyle[] borderLineStyles = null)
        {
            borderLineStyles = borderLineStyles ?? new Excel.XlLineStyle[] { Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous };

            range.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = borderLineStyles[0];
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = borderLineStyles[1];
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = borderLineStyles[2];
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = borderLineStyles[3];
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = borderLineStyles[4];
            range.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = borderLineStyles[5];
        }

        /// <summary>
        /// Set color of boders
        /// </summary>
        /// <param name="borderColors">Colors of [Top, Bottom, Left, Right, InsideHorizontal, InsideVertical]</param>
        public void SetBorderColors(string[] borderColors = null)
        {
            borderColors = borderColors ?? new string[] { "Black", "Black", "Black", "Black", "Black", "Black" };

            range.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.FromName(borderColors[0]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.FromName(borderColors[1]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color.FromName(borderColors[2]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color.FromName(borderColors[3]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Color = Color.FromName(borderColors[4]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlInsideVertical].Color = Color.FromName(borderColors[5]).ToArgb();
        }

        /// <summary>
        /// Set freeze panes
        /// </summary>
        /// <param name="isFreeze">Default  = true</param>
        public void SetFreezePanes(bool isFreeze = true)
        {
            worksheet.Application.ActiveWindow.SplitRow = range.Row;
            worksheet.Application.ActiveWindow.FreezePanes = true;
        }

        public void SetRangeAllFormat(
            string format = "@",
            string backColor = "White",
            string foreColor = "Black",
            bool isFontBold = false,
            bool isFontItalic = false,
            bool isFontUnderline = false,
            Excel.XlHAlign horizontalAlignment = Excel.XlHAlign.xlHAlignLeft,
            Excel.XlHAlign verticalAlignment = Excel.XlHAlign.xlHAlignCenter,
            int columnWith = -1,
            int rowHeight = -1,
            int mergeColumns = 0,
            Excel.XlLineStyle[] borderLineStyles = null,
            string[] borderColors = null,
            bool isFreezePanes = false)
        {
            borderLineStyles = borderLineStyles ?? new Excel.XlLineStyle[] { Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous, Excel.XlLineStyle.xlContinuous };
            borderColors = borderColors ?? new string[] { "Black", "Black", "Black", "Black", "Black", "Black" };

            if (mergeColumns > 0)
            {
                range.Merge(mergeColumns);
            }

            range.NumberFormat = format;

            range.Interior.Color = Color.FromName(backColor).ToArgb();
            range.Font.Color = Color.FromName(foreColor).ToArgb();
            range.Font.Bold = isFontBold;
            range.Font.Italic = isFontItalic;
            range.Font.Underline = isFontUnderline;

            range.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = borderLineStyles[0];
            range.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.FromName(borderColors[0]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = borderLineStyles[1];
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.FromName(borderColors[1]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = borderLineStyles[2];
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color.FromName(borderColors[2]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = borderLineStyles[3];
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color.FromName(borderColors[3]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = borderLineStyles[4];
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Color = Color.FromName(borderColors[4]).ToArgb();
            range.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = borderLineStyles[5];
            range.Borders[Excel.XlBordersIndex.xlInsideVertical].Color = Color.FromName(borderColors[5]).ToArgb();

            range.HorizontalAlignment = horizontalAlignment;
            range.VerticalAlignment = verticalAlignment;

            if (columnWith == -1)
            {
                range.Columns.AutoFit();
            }
            else
            {
                range.ColumnWidth = columnWith;
            }

            if (rowHeight == -1)
            {
                range.Rows.AutoFit();
            }
            else
            {
                range.RowHeight = rowHeight;
            }

            range.Application.ActiveWindow.FreezePanes = isFreezePanes;
        }

        /// <summary>
        /// Set range from start cell to end cell
        /// </summary>
        /// <param name="startRow">Default = 1</param>
        /// <param name="startColumn">Default = 1</param>
        /// <param name="endRow">Default = 1</param>
        /// <param name="endColumn">Default = 1</param>
        public void SetRange(int startRow = 1, int startColumn = 1, int endRow = 1, int endColumn = 1)
        {
            range = (Excel.Range)worksheet.get_Range((Excel.Range)worksheet.Cells[startRow, startColumn], (Excel.Range)worksheet.Cells[endRow, endColumn]);
        }

        /// <summary>
        /// Expand a cell to range
        /// </summary>
        /// <param name="startRow">Default = 1</param>
        /// <param name="startColumn">Default = 1</param>
        /// <param name="rowSpan">Number of rows to expand, default = 1</param>
        /// <param name="columnSpan">Number of columns to expand, default = 1</param>
        public void ExpandCellToRange(int startRow = 1, int startColumn = 1, int rowSpan = 1, int columnSpan = 1)
        {
            SetRange(startRow, startColumn, startRow + rowSpan - 1, startColumn + columnSpan - 1);
        }

        /// <summary>
        /// Set value for cell
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <param name="startRow">Default = 1</param>
        /// <param name="startColumn">Default = 1</param>
        /// <param name="endRow">Default = 1</param>
        /// <param name="endColumn">Default = 1</param>
        public void SetCellValue(object value, int startRow = 1, int startColumn = 1, int endRow = 1, int endColumn = 1)
        {
            SetRange(startRow, startColumn, endRow, endColumn);
            range.Value = value;
        }

        /// <summary>
        /// Set value for array 2D
        /// </summary>
        /// <param name="values">Object 2D Array to set</param>
        /// <param name="startRow">Default = 1</param>
        /// <param name="startColumn">Default = 1</param>
        public void Set2DArrayValue(object[,] values, int startRow = 1, int startColumn = 1)
        {
            SetRange(startRow, startColumn, startRow + values.GetLength(0) - 1, startColumn + values.GetLength(1) - 1);
            range.Value = values;
        }

        /// <summary>
        /// Set value for array 1D
        /// </summary>
        /// <param name="values">Object 1D Array to set</param>
        /// <param name="isSetByColumn">Set by column or set by row, default = true is set by column</param>
        /// <param name="startRow">Default = 1</param>
        /// <param name="startColumn">Default = 1</param>
        public void Set1DArrayValue(object[] values, bool isSetByColumn = true, int startRow = 1, int startColumn = 1)
        {
            int arrayLength = values.Length;

            if (isSetByColumn)
            {
                object[,] obj = new object[arrayLength, 1];

                for (int i = 0; i < arrayLength; i++)
                {
                    obj[i, 0] = values[i];
                }

                SetRange(startRow, startColumn, startRow + arrayLength - 1, startColumn);
                range.Value = obj;
            }
            else
            {
                object[,] obj = new object[1, arrayLength];

                for (int i = 0; i < arrayLength; i++)
                {
                    obj[0, i] = values[i];
                }

                SetRange(startRow, startColumn, startRow, startColumn + arrayLength - 1);
                range.Value = obj;
            }
        }

        /// <summary>
        /// Save workbook as a specified excel file format
        /// </summary>
        /// <param name="xlFileFormat">Excel file format</param>
        public void SaveAs(Excel.XlFileFormat xlFileFormat = Excel.XlFileFormat.xlWorkbookNormal)
        {
            //save as and quit
            workbook.SaveAs(ExportFileName, xlFileFormat);
            workbook.Close();
            ExcelApp.Quit();
            ExcelApp = null;

            //release COM Object
            System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

            //reclaims memory
            GC.Collect();
        }

        /// <summary>
        /// Get workbook as byte array
        /// </summary>
        /// <returns>Byte array</returns>
        public byte[] GetByteArray()
        {
            return File.ReadAllBytes(ExportFileName);
        }
    }
}