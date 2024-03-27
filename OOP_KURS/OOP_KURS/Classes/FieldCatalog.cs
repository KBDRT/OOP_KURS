using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Xml.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace OOP_KURS
{
    // Класс подготовки полей для форм
    public static class FieldCatalog
    {
        static private List<FieldCatalogClass> FieldsValues = new List<FieldCatalogClass>();

        private const string FilePath = @"K:\GitHub\OOP_KURS\OOP_KURS\OOP_KURS\FieldCatalog.xml";

        // Конструктор
        static FieldCatalog()
        {
            GetFieldCatalog();
        }

        // Получить Текст к полям таблицы
        static public void GetFieldCatalog()
        {
            XElement xdoc = XDocument.Load(FilePath).Element("FieldCatalog");

            foreach (XElement ElemSection in xdoc.Elements("Section"))
            {
                FieldCatalogClass CatalogClass = new FieldCatalogClass { ClassName = (string)ElemSection.Attribute("name") };
                foreach (var Elem in ElemSection.Elements())
                {
                    CatalogClass.Elems.Add(new FieldCatalogElem { FieldName = Elem.Name.LocalName, FieldText = Elem.Value });
                }
                FieldsValues.Add(CatalogClass);
            }
        }

        // Получить текст к полю из класса
        static public string GetFieldTextForClass(ref string FieldName, string ClassName)
        {
            FieldCatalogClass CurrentFieldCatalog = FieldsValues.Find(x => x.ClassName == ClassName);

            string Text = null;
            FieldCatalogElem Elem = null;
            if (CurrentFieldCatalog != null)
            {
                ParseFieldNameBtw(ref FieldName);
                Text = FieldName;
                Elem = CurrentFieldCatalog.Elems.Find(x => x.FieldName == Text);
                Text = null;
                if (Elem != null)
                {
                    Text = Elem.FieldText;
                }
            }

            return Text;
        }

        // Получить из строки подстроку между двумя символами
        static private void ParseFieldNameBtw(ref string FieldName, char LeftSymbol = '<', char RightSymbol = '>')
        {
            if (FieldName != null)
            {
                int FirstIndex = FieldName.IndexOf(LeftSymbol) + 1;
                int SecondIndex = FieldName.IndexOf(RightSymbol) - 1;
                if (FirstIndex != 0 && SecondIndex != -2)
                    FieldName = FieldName.Substring(FirstIndex, SecondIndex);
            }
        }

        // Добавить в Grid поля 
        static public void SetColumnsForDataGrid(FilterDataGrid.FilterDataGrid Grid, string ClassName)
        {
            Type type = Type.GetType("OOP_KURS." + ClassName);

            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                string FieldName = field.Name;
                string FieldText = GetFieldTextForClass(ref FieldName, ClassName);

                if (FieldText == null)
                    continue;


                // to:do
                if (FieldName == "LegalAddress")
                    FieldName = "LegalAddress.FullName";

                DataGridTextColumn textColumn = new DataGridTextColumn
                {
                    Header = FieldText,
                    Binding = new Binding(FieldName),
                    Width = DataGridLength.Auto
                };
                Grid.Columns.Add(textColumn);
            }
        }

    }

    class FieldCatalogClass
    {
        public string ClassName;
        public List<FieldCatalogElem> Elems = new List<FieldCatalogElem>();
    }

    class FieldCatalogElem
    {
        public string FieldName;
        public string FieldText;
    }

}
