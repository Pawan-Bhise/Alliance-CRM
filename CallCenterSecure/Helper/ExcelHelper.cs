using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OfficeOpenXml;
using System.Globalization;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

using ClosedXML.Excel;

public static class ExcelHelper
{
    public static int? GetInt(IXLWorksheet ws, int row, int col)
    {
        var cell = ws.Cell(row, col);

        if (cell.IsEmpty())
            return null;

        return int.TryParse(cell.GetString().Trim(), out var i) ? (int?)i : null;
    }

    public static string GetString(IXLWorksheet ws, int row, int col)
    {
        return ws.Cell(row, col).GetString().Trim();
    }

    public static DateTime? GetDate(IXLWorksheet ws, int row, int col)
    {
        var cell = ws.Cell(row, col);

        if (cell.TryGetValue<DateTime>(out var date))
            return date;

        return null;
    }
}


