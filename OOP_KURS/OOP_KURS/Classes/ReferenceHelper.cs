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
            { typeof(Customer),     new CustomerReference() },
            { typeof(TypeDocument), new TypeReference() },
            { typeof(Bank),         new BankReference() },
        };

        private static readonly Dictionary<string, Type> AltComponentsName = new Dictionary<string, Type> { };

        public static void Add(dynamic Elem) => InvokeMethodByElem(Elem, "AddToList");

        public static void Delete(dynamic Elem) => InvokeMethodByElem(Elem, "DeleteFromList");

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

        private static dynamic InvokeMethod(string RefName, string MethodName)
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
            Add(new TypeDocument { Name = "СЧЕТ" });
            Add(new TypeDocument { Name = "АКТ ВЫПОЛНЕННЫХ РАБОТ" });
            Add(new TypeDocument { Name = "ТОВАРНАЯ НАКЛАДНАЯ" });
        }

    }

}
