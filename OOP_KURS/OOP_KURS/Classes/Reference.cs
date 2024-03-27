using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Справочники
    internal abstract class Reference<T>
    {
        private readonly ObservableCollection<T> Elements = new ObservableCollection<T>();

        private readonly Type ElementsType = typeof(T);
        private readonly FieldInfo ID_MethodInfo;

        public void AddToList(dynamic elem)
        {
            if (ID_MethodInfo != null)
                ID_MethodInfo.SetValue(elem, Elements.Count + 1);

            Elements.Add(elem);
        }

        public void DeleteFromList(int ID)
        {
            //Elements.Remove((x) => x == "Bob");
            //var toRemove = Elements.Where(i => typeof(T).ID == ID);

            //foreach (var item in toRemove)
            //    Elements.Remove(item);
        }

        public Reference()
        {
            try
            {
                ID_MethodInfo = ElementsType.GetField("ID");
            }
            catch { }
        }

        public ObservableCollection<T> GetElements()
        {
            return Elements;
        }

    }

    internal class CustomerReference : Reference<Customer> { }
    internal class TypeReference : Reference<TypeDocument> { }
}
