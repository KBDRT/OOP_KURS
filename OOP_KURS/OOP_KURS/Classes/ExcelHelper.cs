using System.Collections.ObjectModel;
using ClosedXML.Excel;
using System;

namespace DocCreator
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
            ws1.Cell(2, 7).Value = string.Format("{0:dd.MM.yyyy}",Doc.CreatedDate);


            int PositionsCount = Doc.Positions.GetElemCount();

            ObservableCollection<Position> Positions = Doc.Positions.GetElements();


            string SumInWords = AmountConverter.Convert(Doc.Positions.TotalSum, out string Kop);


            if (PositionsCount > 1)
            {
                // Вставить строки
                var StartRow = ws1.Row(10);
                StartRow.InsertRowsBelow(PositionsCount - 1);
            }

            for (int i = 0; i < PositionsCount; i++)
            {
                string chel = string.Format("B{0}:E{0}", 10 + i);
                ws1.Range(chel).Merge();

                var row = ws1.Row(10 + i);
                row.Height = 36.75;

                ws1.Cell(10 + i, 1).Value = i + 1;

                ws1.Cell(10 + i, 2).Value = Positions[i].FullName;

                ws1.Cell(10 + i, 6).Value = Positions[i].Quantity;

                //ws1.Cell(10 + i, 7).Value = Doc.Positions[i].UnitOfMeasurement.FullName;

                ws1.Cell(10 + i, 8).Value = Math.Round(Positions[i].Amount, 2) ;

                ws1.Cell(10 + i, 9).Value = Math.Round(Positions[i].TotalAmount, 2);
            }

            ws1.Cell(10 + PositionsCount, 9).Value = ws1.Cell(12 + PositionsCount, 9).Value = Doc.Positions.TotalSum;

            ws1.Cell(14 + PositionsCount, 4).Value = (int)Doc.Positions.TotalSum;

            ws1.Cell(14 + PositionsCount, 6).Value = Kop;


            ws1.Cell(15 + PositionsCount, 3).Value = SumInWords;



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
