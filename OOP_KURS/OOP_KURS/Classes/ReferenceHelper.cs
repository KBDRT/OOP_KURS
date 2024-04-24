using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace OOP_KURS
{
    // Работа со справочниками
    static public class ReferenceHelper
    {
        private static readonly Dictionary<Type, object> Components = new Dictionary<Type, object>
        {
            { typeof(Customer),          new CustomerReference() },
            { typeof(TypeDocument),      new TypeReference() },
            { typeof(Bank),              new BankReference() },
            { typeof(Unit),              new UnitReference() },
            { typeof(ProductAndService), new ProductAndServiceReference() },
            { typeof(Document),          new DocumentReference() },
        };

        private static readonly Dictionary<string, Type> AltComponentsName = new Dictionary<string, Type> { };

        public static void Add(dynamic Elem) => InvokeMethodByElem(Elem, "AddToList");

        public static void Delete(dynamic Elem) => InvokeMethodByElem(Elem, "DeleteFromList");

        public static void AddCopy(dynamic Elem)
        {
            Type ElemType = Elem.GetType();

            object ElemCopy = Elem.Clone();

            foreach (PropertyInfo fi in ElemType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.ToArray())
            {
                if (!fi.CanWrite)
                    continue;

                if (!fi.PropertyType.FullName.ToString().StartsWith("System."))
                {
                    Type ElemType2 = fi.PropertyType; // Тип глубокого поля

                    dynamic value = fi?.GetValue(Elem); // Получить значение из копируемого

                    object ElemCopyDeep = value?.Clone(); // Клонировать

                    fi.SetValue(ElemCopy, ElemCopyDeep); // Присвоить в новый объект

                }
            }
            Add(ElemCopy);
        }


        private static void InvokeMethodByElem(dynamic Elem, string MethodName)
        {
            try
            {
                dynamic Instance = Components[Elem.GetType()];
                MethodInfo Method = Instance.GetType().GetMethod(MethodName);
                Method?.Invoke(Instance, new object[] { Elem });
            }
            catch { }
        }

        public static dynamic GetElementsByRefName(string RefName)
        {
            return InvokeMethod(RefName, "GetElements");
        }

        public static dynamic InvokeMethod(string RefName, string MethodName)
        {
            try
            {
                MethodInfo Method = Components[AltComponentsName[RefName]].GetType().GetMethod(MethodName);
                return Method?.Invoke(Components[AltComponentsName[RefName]], new object[] { });
            }
            catch
            {
                return null;
            }
        }

        public static void InvokeMethod(string RefName, string MethodName, dynamic Params)
        {
            try
            {
                MethodInfo Method = Components[AltComponentsName[RefName]].GetType().GetMethod(MethodName);
                Method?.Invoke(Components[AltComponentsName[RefName]], new object[] { Params });
            }
            catch
            {

            }
        }

        static ReferenceHelper()
        {
            FillAltNameDict();
            InitTypeDocs();
        }

        private static void FillAltNameDict()
        {
            foreach (var Comp in Components)
            {
                AltComponentsName.Add(Comp.Key.Name, Comp.Key);
            }
        }

        private static void InitTypeDocs()
        {
            Add(new TypeDocument { Name = "Счет" });
            Add(new TypeDocument { Name = "Акт выполненных работ" });
            Add(new TypeDocument { Name = "Товарная накладная" });
        }

    }

}
