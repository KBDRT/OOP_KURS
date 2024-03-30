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
    public class FieldCatalogClass
    {
        public string ClassName;
        public List<FieldCatalogElem> Elems = new List<FieldCatalogElem>();
    }

    public class FieldCatalogElem
    {
        public string FieldName;
        public string FieldText;
        public List<FieldCatalogAttr> FieldAttr = new List<FieldCatalogAttr>();
    }

    public class FieldCatalogAttr
    {
        public string Name;
        public string Value;
    }

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
                foreach (XElement Elem in ElemSection.Elements())
                {

                    List<FieldCatalogAttr> AttrList = new List<FieldCatalogAttr>();

                    foreach (XAttribute Attr in Elem.Attributes())
                    {
                        AttrList.Add(new FieldCatalogAttr { Name = Attr.Name.ToString(), Value = Attr.Value });
                    }

                    CatalogClass.Elems.Add(new FieldCatalogElem { FieldName = Elem.Name.LocalName, FieldText = Elem.Value, FieldAttr = AttrList });

                }
                FieldsValues.Add(CatalogClass);
            }
        }

        // Получить текст к полю из класса
        static public string GetFieldTextByFC(ref string FieldName, FieldCatalogClass FC, out FieldCatalogElem Elem)
        {
            string Text = null;
            Elem = null;
            if (FC != null)
            {
                ParseFieldNameBtw(ref FieldName);
                Text = FieldName;
                Elem = GetFieldCatalogElem(FC, Text);
                if (Elem != null)
                {
                    Text = Elem.FieldText;
                    FieldName = GetAttrValueForElem("BindingName", Elem) ?? FieldName; // Подменить имя поля по атрибуту для привязки
                }
                else
                {
                    Text = null;
                }
            }

            return Text;
        }

        static private FieldCatalogClass GetFieldCatalog(string ClassName)
        {
            return FieldsValues.Find(x => x.ClassName == ClassName);
        }

        static private FieldCatalogElem GetFieldCatalogElem(FieldCatalogClass FieldCatalog, string FieldName)
        {
            if (FieldCatalog is null || String.IsNullOrEmpty(FieldName))
                return null;
            return FieldCatalog.Elems.Find(x => x.FieldName == FieldName);
        }


        // Получить атрибуты элемента
        static private string GetAttrValueForElem(string AttrName, FieldCatalogElem Elem)
        {
            if (Elem.FieldAttr.Count == 0)
                return null;

            FieldCatalogAttr Attr = Elem.FieldAttr.Find(x => x.Name == AttrName);
            if (Attr != null)
                return Attr.Value;
            return null;
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

        static private bool CheckReadOnlyField(FieldCatalogElem Elem)
        {
            string val = GetAttrValueForElem("ReadOnly", Elem);

            return val == "X";
        }

        // Добавить в Grid поля 
        static public void SetColumnsForDataGrid(FilterDataGrid.FilterDataGrid Grid, string ClassName)
        {
            Type type = Type.GetType("OOP_KURS." + ClassName);

            FieldCatalogClass CurrentFieldCatalog = GetFieldCatalog(ClassName);

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                FieldCatalogElem Element;
                string FieldName = property.Name;
                string FieldText = GetFieldTextByFC(ref FieldName, CurrentFieldCatalog, out Element);

                if (FieldText == null)
                    continue;

                DataGridTextColumn textColumn = new DataGridTextColumn
                {
                    Header = FieldText,
                    Binding = new Binding(FieldName),
                    Width = DataGridLength.Auto,
                    IsReadOnly = CheckReadOnlyField(Element)
                };
                Grid.Columns.Add(textColumn);
            }
        }

    }

}
