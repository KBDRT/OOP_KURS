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
        };

        private static readonly Dictionary<string, Type> AltComponentsName = new Dictionary<string, Type>
        {
            { "Customer", typeof(Customer)    },
            { "TypeDocument", typeof(TypeDocument) },
        };

        public static void Add(dynamic Elem)
        {
            try
            {
                dynamic Instance = Components[Elem.GetType()];
                MethodInfo Method = Instance.GetType().GetMethod("AddToList");
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
            MethodInfo Method = Components[AltComponentsName[RefName]].GetType().GetMethod(MethodName);
            return Method?.Invoke(Components[AltComponentsName[RefName]], new object[] {  });
        }
        



    }

}
