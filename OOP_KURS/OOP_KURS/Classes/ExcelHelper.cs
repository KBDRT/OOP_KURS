using System.Collections.ObjectModel;
using ClosedXML.Excel;
using System;

namespace DocCreator
{
    public static class ExcelHelper
    {

        public static void Start(Document Doc)
        {

            if (Doc.Type?.ID == null)
                return;

            XLWorkbook wbook = new XLWorkbook(@"K:\GitHub\OOP_KURS\OOP_KURS\OOP_KURS\Temp.xlsx");

            IXLWorksheet FirstSheet = wbook.Worksheet(1);

            FillAcc(FirstSheet, Doc);

            //switch (Doc.Type.ID)
            //{
            //    case 1:
            //        FillAcc(FirstSheet, Doc);
            //        break;
            //    case 2:
            //        FillAct(FirstSheet, Doc);
            //        break;
            //    default:
            //        return;
            //}

            FirstSheet.SheetView.SetView(XLSheetViewOptions.PageBreakPreview);

            wbook.Save();
        }

        //private static void FillNak(IXLWorksheet Sheet, Document DocData)
        //{

        //}

        private static string GetDateWithFormat(DateTime? Date)
        {
            return string.Format("{0:dd.MM.yyyy}", Date);
        }

        private static void FillAcc(IXLWorksheet Sheet, Document DocData)
        {
            
        }

        private static void FillAct(IXLWorksheet Sheet, Document DocData)
        {
            // АКТ
            Sheet.Cell(2, 5).Value = DocData.Number;
            Sheet.Cell(2, 7).Value = GetDateWithFormat(DocData.DocDate);

            string Customer = string.Format("{0} \"{1}\", ИНН {2}", DocData.Client.Form, DocData.Client.Name, DocData.Client.INN);

            if (DocData.Client.KPP != null)
                Customer += ", КПП " + DocData.Client.KPP;

            Sheet.Cell(5, 3).Value = Customer;



            int PositionsCount = DocData.Positions.GetElemCount();

            ObservableCollection<Position> Positions = DocData.Positions.GetElements();

            string SumInWords = AmountConverter.Convert(DocData.Positions.TotalSum, out string Kop);

            if (PositionsCount > 1)
            {
                // Вставить строки
                var StartRow = Sheet.Row(10);
                StartRow.InsertRowsBelow(PositionsCount - 1);
            }

            for (int i = 0; i < PositionsCount; i++)
            {
                string chel = string.Format("B{0}:E{0}", 10 + i);
                Sheet.Range(chel).Merge();

                var row = Sheet.Row(10 + i);
                row.Height = 36.75;

                Sheet.Cell(10 + i, 1).Value = i + 1;

                Sheet.Cell(10 + i, 2).Value = Positions[i].FullName;

                Sheet.Cell(10 + i, 6).Value = Positions[i].Quantity;

                Sheet.Cell(10 + i, 7).Value = Positions[i].UnitOfMeasurement.FullName;

                Sheet.Cell(10 + i, 8).Value = Math.Round(Positions[i].Amount, 2);

                Sheet.Cell(10 + i, 9).Value = Math.Round(Positions[i].TotalAmount, 2);
            }

            Sheet.Cell(10 + PositionsCount, 9).Value = Sheet.Cell(12 + PositionsCount, 9).Value = DocData.Positions.TotalSum;

            Sheet.Cell(14 + PositionsCount, 4).Value = (int)DocData.Positions.TotalSum;

            Sheet.Cell(14 + PositionsCount, 6).Value = Kop;

            Sheet.Cell(15 + PositionsCount, 3).Value = SumInWords;

            Sheet.Cell(20 + PositionsCount, 1).Value = DocData.Client.ViewName;
            Sheet.Cell(21 + PositionsCount, 1).Value = "Юридический адрес:" + DocData.Client.LegalAddress.PostalCode + ", " + DocData.Client.LegalAddress.Region + ",";
            Sheet.Cell(22 + PositionsCount, 1).Value = "г. " + DocData.Client.LegalAddress.City + ", ул." + DocData.Client.LegalAddress.Street + ", д." + DocData.Client.LegalAddress.House;

            Sheet.Cell(23 + PositionsCount, 1).Value = "тел. " + DocData.Client.Phone;

            Sheet.Cell(24 + PositionsCount, 1).Value = "ИНН:" + DocData.Client.INN;

            if (DocData.Client.KPP != null)
                Sheet.Cell(24 + PositionsCount, 1).Value += " КПП:" + DocData.Client.KPP;

            Sheet.Cell(25 + PositionsCount, 1).Value = "Банк получателя: " + DocData.Client.Bank.Name;

            Sheet.Cell(26 + PositionsCount, 1).Value = "р/счет: " + DocData.Client.PaymentAccount;

            Sheet.Cell(27 + PositionsCount, 1).Value = "к/счет: " + DocData.Client.CorrespondentAccount;

            Sheet.Cell(28 + PositionsCount, 1).Value = "БИК: " + DocData.Client.Bank.BIK + ", ОГРН: " + DocData.Client.OGR_Number;

            Sheet.Cell(29 + PositionsCount, 1).Value = DocData.Client.CompanyRepresentative.Position;

            Sheet.Cell(30 + PositionsCount, 1).Value = "______________________ " + DocData.Client.CompanyRepresentative.ViewFIO;
        }


    }
}
