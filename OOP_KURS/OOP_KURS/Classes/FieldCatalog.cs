using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace DocCreator
{
    public class FieldCatalogClass
    {
        public string ClassName;
        public List<FieldCatalogElem> Elems = new List<FieldCatalogElem>();
        public List<FieldCatalogAttr> Properties = new List<FieldCatalogAttr>();
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
        private static List<FieldCatalogClass> ClassValues = new List<FieldCatalogClass>();

        private static string FilePath = Environment.CurrentDirectory.ToString() + @"\FieldCatalog.xml";

        // Конструктор
        static FieldCatalog()
        {
            GetFieldCatalog();
        }

        // Получить Текст к полям таблицы
        public static void GetFieldCatalog()
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

                foreach (XAttribute Attr in ElemSection.Attributes())
                {
                    if (Attr.Name.ToString() != "name")
                        CatalogClass.Properties.Add(new FieldCatalogAttr { Name = Attr.Name.ToString(), Value = Attr.Value });
                }

                ClassValues.Add(CatalogClass);
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
                Text = Elem?.FieldText;
                FieldName = GetAttrValueForElem("BindingName", Elem) ?? FieldName; // Подменить имя поля по атрибуту для привязки
            }

            return Text;
        }

        static private FieldCatalogClass GetFieldCatalog(string ClassName)
        {
            return ClassValues.Find(x => x.ClassName == ClassName);
        }

        static private FieldCatalogElem GetFieldCatalogElem(FieldCatalogClass FieldCatalog, string FieldName)
        {
            if (FieldCatalog is null || string.IsNullOrEmpty(FieldName))
                return null;
            return FieldCatalog.Elems.Find(x => x.FieldName == FieldName);
        }

        // Получить атрибуты элемента
        static private dynamic GetAttrValueForElem(string AttrName, FieldCatalogElem Elem)
        {
            if (Elem?.FieldAttr.Count == 0)
                return null;

            FieldCatalogAttr Attr = Elem?.FieldAttr.Find(x => x.Name == AttrName);
            return Attr?.Value;
        }

        static private dynamic GetAttrValueForSect(string ClassName, string AttrName)
        {
            return GetFieldCatalog(ClassName)?.Properties?.Find(x => x.Name == AttrName)?.Value;
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

        // Закрыть на ввод поле
        private static bool CheckReadOnlyField(FieldCatalogElem Elem)
        {
            return GetAttrValueForElem(Elem, "ReadOnly");
        }

        private static bool GetAttrValueForElem(FieldCatalogElem Elem, string AttrName)
        {
            string val = GetAttrValueForElem(AttrName, Elem);

            return val == "X";
        }

        // Ширина столбца
        private static DataGridLength GetWidth(FieldCatalogElem Elem)
        {
            string val = GetAttrValueForElem("WidthStar", Elem);
            double width = 1.0;
            try
            {
                width = val != null ? Convert.ToDouble(val) : 1.0;
            }
            catch { }
            return new DataGridLength(width, DataGridLengthUnitType.Star);
        }

        // Добавить в Grid поля 
        static public void SetColumnsForDataGrid(DataGrid Grid, string ClassName)
        {
            Type type = Type.GetType("DocCreator." + ClassName);

            if (type == null)
                return;

            FieldCatalogClass CurrentFieldCatalog = GetFieldCatalog(ClassName);

            bool UsingDisplayIndex = Convert.ToBoolean(GetAttrValueForSect(ClassName, "UseDisplayIndex")) ?? false;
            string UsingReadOnly = GetAttrValueForSect(ClassName, "ReadOnly");

            foreach (PropertyInfo property in type?.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                string FieldName = property.Name;
                string FieldText = GetFieldTextByFC(ref FieldName, CurrentFieldCatalog, out FieldCatalogElem Element);

                if (FieldText == null)
                    continue;

                DataGridTextColumn textColumn = new DataGridTextColumn
                {
                    Header = FieldText,
                    Binding = new Binding(FieldName),
                    Width = GetWidth(Element),
                    CanUserSort = !GetAttrValueForElem(Element, "NoUserSort")
                };

                textColumn.IsReadOnly = UsingReadOnly == "X" ? true : CheckReadOnlyField(Element);

                textColumn.Binding.StringFormat = GetAttrValueForElem("StringFormat", Element); // Формат строки

                if (UsingDisplayIndex)
                {
                    textColumn.DisplayIndex = Convert.ToInt32(GetAttrValueForElem("DisplayIndex", Element)) - 1; // Порядок столбца
                }

                Grid.Columns.Add(textColumn);
            }
        }

        // Установка свойств для окна формы
        public static void SetPropertiesForWindow(Window Window, string ClassName)
        {
            FieldCatalogClass FC = GetFieldCatalog(ClassName);

            if (FC == null)
                return;

            Type myType = typeof(Window);
            foreach (FieldCatalogAttr prop in FC?.Properties)
            {
                PropertyInfo Info = myType?.GetProperty(prop.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                try
                {
                    Info?.SetValue(Window, Convert.ToDouble(prop.Value));
                }
                catch { }
            }
        }

    }

}
