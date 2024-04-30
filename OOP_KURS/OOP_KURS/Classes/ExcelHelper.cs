using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;


namespace OOP_KURS
{
    static class ExcelHelper
    {

        public static void Start(Document Doc)
        {

            XLWorkbook wbook = new XLWorkbook(@"K:\GitHub\OOP_KURS\OOP_KURS\OOP_KURS\Temp.xlsx");

            //

            var ws1 = wbook.Worksheet(1);


            // АКТ
            ws1.Cell(2,5).Value = Doc.Number;
            ws1.Cell(2, 7).Value = Doc.CreatedDate;

            if (Doc.Positions.Count > 1)
            {
                // Вставить строки
                var StartRow = ws1.Row(10);
                StartRow.InsertRowsBelow(Doc.Positions.Count - 1);
            }

            for (int i = 0; i < Doc.Positions.Count; i++)
            {
                string chel = string.Format("B{0}:E{0}", 10 + i);
                ws1.Range(chel).Merge();

                var row = ws1.Row(10 + i);
                row.Height = 36.75;

                ws1.Cell(10 + i, 1).Value = i + 1;

                ws1.Cell(10 + i, 2).Value = Doc.Positions[i].FullName;

                ws1.Cell(10 + i, 6).Value = Doc.Positions[i].Quantity;

                //ws1.Cell(10 + i, 7).Value = Doc.Positions[i].UnitOfMeasurement.FullName;

                ws1.Cell(10 + i, 8).Value = Doc.Positions[i].Amount;

                ws1.Cell(10 + i, 9).Value = Doc.Positions[i].TotalAmount;
            }

            // StartRow.CopyTo(EndRow);


            //StartRow.InsertRowsBelow(4);

            ////var doc = ReferenceHelper.GetElementsByRefName()

            //for (int i = 1; i <= Doc.Positions.Count; i++)
            //{
            //    if (i <> 1)
            //    {
            //        StartRow.Inse
            //    }
            //}

            //

            //var targetCell = ws1.Range("A11:I11");
            //sourceCell.CopyTo(targetCell);

            //targetCell = ws1.Range("A12:I12");
            //sourceCell.CopyTo(targetCell);

            //targetCell = ws1.Range("A13:I13");
            //sourceCell.CopyTo(targetCell);

            //targetCell = ws1.Range("A14:I14");
            //sourceCell.CopyTo(targetCell);



            ws1.SheetView.SetView(XLSheetViewOptions.PageBreakPreview);

            wbook.Save();
        }
    }
}
