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

        public void AddToList(T elem)
        {
            ID_MethodInfo?.SetValue(elem, Convert.ToUInt16(Elements.Count + 1));
            Elements.Add(elem);
        }

        public void DeleteFromList(T elem)
        {
            Elements.Remove(elem);
        }

        public void ClearList()
        {
            Elements.Clear();
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
    internal class BankReference : Reference<Bank> { }
    internal class UnitReference : Reference<Unit> { }
    internal class ProductAndServiceReference : Reference<ProductAndService> { }
    internal class DocumentReference : Reference<Document> { }
}
