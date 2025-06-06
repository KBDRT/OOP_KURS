﻿using System.Collections.ObjectModel;
using ClosedXML.Excel;
using System;
using Microsoft.Win32;

namespace DocCreator
{
    public class ExcelHelper
    {
        private readonly XLWorkbook wbook;
        private readonly IXLWorksheet Sheet;
        private readonly Document DocData;

        private delegate void Fill();

        public ExcelHelper(Document Doc)
        {

            if (Doc == null || Doc.Type?.ID == null || (Doc.Type?.ID != 1 && Doc.Type?.ID != 2))
                return;

            wbook = new XLWorkbook(Doc.Type.ExcelTemplatePath);
            Sheet = wbook?.Worksheet(1);
            DocData = Doc;
            Fill InfoFill;

            switch (Doc.Type.ID)
            {
                case 1:
                    InfoFill = FillAcc;
                    break;
                case 2:
                    InfoFill = FillAct;
                    break;
                default:
                    return;
            }

            string FileName = OpenSaveDialog();
            if (string.IsNullOrEmpty(FileName))
                return;

            InfoFill();

            Sheet.SheetView.SetView(XLSheetViewOptions.PageBreakPreview);
            wbook.SaveAs(FileName);
        }

        private string GetDateWithFormat(DateTime? Date)
        {
            return string.Format("{0:dd.MM.yyyy}", Date);
        }

        private string GetCustomerINFO(Customer Client)
        {
            if (Client == null)
                return "";

            string Customer = string.Format("{0} \"{1}\", ИНН {2}", Client.Form.ShortName, Client.Name, Client.INN);

            if (Client.KPP != null)
                Customer += ", КПП " + Client.KPP;

            return Customer;
        }

        private void MergeCell(string Cell, int RowInd)
        {
            string CellRange = string.Format(Cell, RowInd);
            Sheet.Range(CellRange).Merge();
        }

        private void SetHeightRow(int RowInd, double Height = 33.75)
        {
            Sheet.Row(RowInd).Height = Height;
        }

        private void FillAcc()
        {
            Customer Org = ReferenceHelper.Organization;
            Address Address = Org.LegalAddress;

            // Header
            Sheet.Cell(1, 1).Value = Sheet.Cell(11, 1).Value = Org.Form.Name + " " + Org.Name;
            Sheet.Cell(3, 1).Value = string.Format("{0}, {1}, г.{2}, ул.{3}, д.{4}, кв.{5}", Address.PostalCode, Address.Region, Address.City, Address.Street, Address.House, Address.AppartNumber);
            Sheet.Cell(4, 1).Value = "Телефон: " + Org.Phone;
            Sheet.Cell(6, 1).Value = Org.Bank.Name;
            Sheet.Cell(6, 20).Value = Org.Bank.BIK;
            Sheet.Cell(9, 9).Value = Org.INN;
            Sheet.Cell(7, 20).Value = Org.CorrespondentAccount;
            Sheet.Cell(9, 20).Value = Org.PaymentAccount;
            Sheet.Cell(20, 1).Value = "СЧЕТ № " + DocData.Number + " от " + GetDateWithFormat(DocData.DocDate);
            Sheet.Cell(22, 6).Value = GetCustomerINFO(DocData.Client);
            Sheet.Cell(23, 6).Value = GetCustomerINFO(Org);

            // Positions
            int PositionsCount = DocData.Positions.GetElemCount();
            ObservableCollection<Position> Positions = DocData.Positions.GetElements();
            string SumInWords = Convert(DocData.Positions.TotalSum, out _);

            if (PositionsCount > 1)
                Sheet.Row(27).InsertRowsBelow(PositionsCount - 1);

            string[] MergeCellsRange = { "B{0}:L{0}", "M{0}:P{0}", "Q{0}:U{0}", "V{0}:AC{0}", "AD{0}:AJ{0}" };
            for (int i = 0; i < PositionsCount; i++)
            {

                foreach (string str in MergeCellsRange)
                    MergeCell(str, 27 + i);

                SetHeightRow(27 + i);
                Sheet.Cell(27 + i, 1).Value = i + 1;
                Sheet.Cell(27 + i, 2).Value = Positions[i].FullName;
                Sheet.Cell(27 + i, 13).Value = Positions[i].UnitOfMeasurement;
                Sheet.Cell(27 + i, 17).Value = Positions[i].Quantity;
                Sheet.Cell(27 + i, 22).Value = Math.Round(Positions[i].Amount, 2);
                Sheet.Cell(27 + i, 30).Value = Math.Round(Positions[i].TotalAmount, 2);
            }
            Sheet.Cell(27 + PositionsCount, 30).Value = DocData.Positions.TotalSum;
            Sheet.Cell(31 + PositionsCount, 1).Value = SumInWords;

            // Footer
            Sheet.Cell(34 + PositionsCount, 1).Value = Org.CompanyRepresentative.Position;
            Sheet.Cell(34 + PositionsCount, 19).Value = Org.CompanyRepresentative.ViewFIO;
        }

        private void FillAct()
        {
            // Header
            Sheet.Cell(2, 5).Value = DocData.Number;
            Sheet.Cell(2, 7).Value = GetDateWithFormat(DocData.DocDate);
            Sheet.Cell(4, 3).Value = ReferenceHelper.Organization.ViewName;
            Sheet.Cell(5, 3).Value = GetCustomerINFO(DocData.Client);

            // Positions
            int PositionsCount = DocData.Positions.GetElemCount();
            ObservableCollection<Position> Positions = DocData.Positions.GetElements();
            string SumInWords = Convert(DocData.Positions.TotalSum, out string Kop);

            if (PositionsCount > 1)
                Sheet.Row(10).InsertRowsBelow(PositionsCount - 1);

            for (int i = 0; i < PositionsCount; i++)
            {
                MergeCell("B{0}:E{0}", 10 + i);
                SetHeightRow(10 + i, 36.75);
                Sheet.Cell(10 + i, 1).Value = i + 1;
                Sheet.Cell(10 + i, 2).Value = Positions[i].FullName;
                Sheet.Cell(10 + i, 6).Value = Positions[i].Quantity;
                Sheet.Cell(10 + i, 7).Value = Positions[i].UnitOfMeasurement;
                Sheet.Cell(10 + i, 8).Value = Math.Round(Positions[i].Amount, 2);
                Sheet.Cell(10 + i, 9).Value = Math.Round(Positions[i].TotalAmount, 2);
            }
            Sheet.Cell(10 + PositionsCount, 9).Value = Sheet.Cell(12 + PositionsCount, 9).Value = DocData.Positions.TotalSum;
            Sheet.Cell(14 + PositionsCount, 4).Value = (int)DocData.Positions.TotalSum;
            Sheet.Cell(14 + PositionsCount, 6).Value = Kop;
            Sheet.Cell(15 + PositionsCount, 3).Value = SumInWords;

            // Footer
            FillCompanyInfoForAct(DocData.Client, 20, 1, PositionsCount);
            FillCompanyInfoForAct(ReferenceHelper.Organization, 20, 5, PositionsCount);
        }

        private void FillCompanyInfoForAct(Customer Company, int StartRow, int ColumnIndex, int RowCount)
        {

            Sheet.Cell(StartRow + RowCount, ColumnIndex).Value = Company.ViewName;
            Sheet.Cell(StartRow + 1 + RowCount, ColumnIndex).Value = "Юридический адрес:" + Company.LegalAddress.PostalCode + ", " + Company.LegalAddress.Region + ",";
            Sheet.Cell(StartRow + 2 + RowCount, ColumnIndex).Value = "г. " + Company.LegalAddress.City + ", ул." + Company.LegalAddress.Street + ", д." + Company.LegalAddress.House;
            Sheet.Cell(StartRow + 3 + RowCount, ColumnIndex).Value = "тел. " + Company.Phone;
            Sheet.Cell(StartRow + 4 + RowCount, ColumnIndex).Value = "ИНН:" + Company.INN;

            if (Company.KPP != null)
                Sheet.Cell(StartRow + 4 + RowCount, ColumnIndex).Value += " КПП:" + Company.KPP;

            Sheet.Cell(StartRow + 5 + RowCount, ColumnIndex).Value = "Банк получателя: " + Company.Bank.Name;
            Sheet.Cell(StartRow + 6 + RowCount, ColumnIndex).Value = "р/счет: " + Company.PaymentAccount;
            Sheet.Cell(StartRow + 7 + RowCount, ColumnIndex).Value = "к/счет: " + Company.CorrespondentAccount;
            Sheet.Cell(StartRow + 8 + RowCount, ColumnIndex).Value = "БИК: " + Company.Bank.BIK + ", ОГРН: " + Company.OGR_Number;
            Sheet.Cell(StartRow + 9 + RowCount, ColumnIndex).Value = Company.CompanyRepresentative.Position;
            Sheet.Cell(StartRow + 10 + RowCount, ColumnIndex).Value = "______________________ " + Company.CompanyRepresentative.ViewFIO;
        }

        private string OpenSaveDialog()
        {
            SaveFileDialog DialogObj = new SaveFileDialog
            {
                Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*",
                FileName = FormFileName(),
                AddExtension = true,
                DefaultExt = ".xlsx",
            };

            if (DialogObj?.ShowDialog() != true)
                DialogObj.FileName = "";

            return DialogObj?.FileName;
        }

        private string FormFileName()
        {
            return string.Format("{0} №{1}, {2} '{3}', {4}", DocData.Type.FileName, DocData.Number, DocData.Client?.Form.ShortName, DocData.Client?.Name, GetDateWithFormat(DocData.DocDate));
        }


        public string Convert(double Sum, out string Kop)
        {
            byte FractPart, Kop1, Kop2;
            string KopFull;
            int FixPart = (int)Sum; // Целая часть суммы
            Sum = Math.Round(Sum, 2); // Округление до 2 знаков после запятой
            FractPart = (byte)(Math.Round(Sum - FixPart, 2) * 100); // Дробная часть
            Kop1 = (byte)(FractPart / 10); // Десятая дробная часть
            Kop2 = (byte)(FractPart % 10); // Сотая дробная часть

            // Определение копеек
            if (Kop1 != 1 && Kop2 == 1)
            {
                KopFull = " копейка";
            }
            else if (Kop1 != 1 && Kop2 > 1 && Kop2 < 5)
            {
                KopFull = " копейки";
            }
            else
            {
                KopFull = " копеек";
            }

            KopFull = string.Format("{0}{1}{2}", Kop1, Kop2, KopFull);
            Kop = string.Format("{0}{1}", Kop1, Kop2);

            // Разделение целой суммы по 3 цифрам
            int[] Sum_3x = new int[4];
            for (byte i = 0; i < 4; i++)
            {
                Sum = (double)FixPart / 1000;
                FixPart = (int)Sum;
                Sum_3x[i] = (int)(Math.Round(Sum - FixPart, 3) * 1000);
            }

            string[] TextTemp = new string[4];
            byte Numb1, Numb2, Numb3;

            string[] T1 = { "", "сто ", "двести ", "триста ", "четыреста ", "пятьсот ", "шестьсот ", "семьсот ", "восьмесот ", "девятьсот " };
            string[] T2 = { "", "", "двадцать ", "тридцать ", "сорок ", "пятьдесят ", "шестьдесят ", "семьдесят ", "восьмедясят ", "девяносто " };


            string Text0 = "";
            if (Sum_3x[0] + Sum_3x[1] + Sum_3x[2] + Sum_3x[3] == 0)
                Text0 = "ноль рублей " + KopFull;
            else
            { 
                for (byte i = 0; i < 4; i++)
                {
                    Numb1 = (byte)(Sum_3x[i] % 10); // Единичный разряд
                    Numb2 = (byte)((Sum_3x[i] - Numb1) / 10 % 10); // Десятичный
                    Numb3 = (byte)(Sum_3x[i] / 100); // Сотый
                    TextTemp[i] = T1[Numb3] + T2[Numb2];

                    TextTemp[i] += GetFirstText(Numb1, Numb2, i);
                    TextTemp[i] += GetSecondText(Numb1, Numb2, Numb3, i);
                }

                for (sbyte i = 3; i >= 0; i--)
                {
                    Text0 += TextTemp[i];
                }
                Text0 += KopFull;
            }

            return Text0;
        }

        private string GetFirstText(byte N1, byte N2, byte i )
        {
            string[] T3 = { "десять ", "одиннадцать ", "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ", "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать " };
            string[] T4 = { "", "одна ", "две ", "три ", "четыре ", "пять ", "шесть ", "семь ", "восемь ", "девять " };
            string[] T5 = { "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ", "семь ", "восемь ", "девять " };
            string Val = "";

            if (N1 == 1)
                Val = T3[N1];
            else if (N2 != 1 && i == 1)
                Val = T4[N1];
            else
                Val = T5[N1];

            return Val;
        }


        private string GetSecondText(byte N1, byte N2, byte N3, byte i)
        {
            string Val = "";
            string[] T6 = { "рубль ", "тысяча ", "миллион ", "миллиард" };
            string[] T7 = { "рубля ", "тысячи ", "миллиона ", "миллиарда" };
            string[] T8 = { "рублей ", "", "", "" };
            string[] T9 = { "рублей ", "тысяч ", "миллионов ", "миллиардов" };

            if (N2 != 1 && N1 == 1)
                Val = T6[i];
            else if (N2 != 1 && N1 > 1 && N1 < 5)
                Val = T7[i];
            else if (N1 == 0 && N2 == 0 && N3 == 0)
                Val = T8[i];
            else
                Val = T9[i];


            return Val;
        }
    }
}
