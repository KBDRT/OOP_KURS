using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace OOP_KURS
{
    // Справочники
    internal abstract class Reference<T>
    {
        private readonly ObservableCollection<T> Elements = new ObservableCollection<T>();

        private readonly Type ElementsType = typeof(T);
        private readonly PropertyInfo ID_MethodInfo;

        public void AddToList(T elem)
        {
            //ID_MethodInfo?.SetValue(elem, Convert.ToUInt16(Elements.Count + 1));
            Elements.Add(elem);
        }

        public void DeleteFromListWithID(int indx)
        {
            Elements.RemoveAt(indx - 1);
        }

        public void DeleteFromList(T elem)
        {
            Elements.Remove(elem);
        }

        public void ClearList()
        {
            Elements.Clear();
        }

        public Reference(string PropertyName = "ID")
        {
            try
            {
                ID_MethodInfo = ElementsType.GetProperty(PropertyName);
            }
            catch { }
            Elements.CollectionChanged += ElementsCollectionChanged;
        }

        public ObservableCollection<T> GetElements()
        {
            return Elements;
        }

        private void ElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int i = 0;
            foreach (var Elem in Elements)
            {
                i++;
                ID_MethodInfo?.SetValue(Elem, Convert.ToUInt16(i));
            }
            //switch (e.Action)
            //{
            //    case NotifyCollectionChangedAction.Add: // если добавление
            //        if (e.NewItems?[0] is Person newPerson)
            //            Console.WriteLine($"Добавлен новый объект: {newPerson.Name}");
            //        break;
            //    case NotifyCollectionChangedAction.Remove: // если удаление
            //        if (e.OldItems?[0] is Person oldPerson)
            //            Console.WriteLine($"Удален объект: {oldPerson.Name}");
            //        break;
            //    case NotifyCollectionChangedAction.Replace: // если замена
            //        if ((e.NewItems?[0] is Person replacingPerson) &&
            //            (e.OldItems?[0] is Person replacedPerson))
            //            Console.WriteLine($"Объект {replacedPerson.Name} заменен объектом {replacingPerson.Name}");
            //        break;
            //}
        }

    }

    internal class CustomerReference : Reference<Customer> { }
    internal class TypeReference : Reference<TypeDocument> { }
    internal class BankReference : Reference<Bank> { }
    internal class UnitReference : Reference<Unit> { }
    internal class ProductAndServiceReference : Reference<ProductAndService> { }
    internal class DocumentReference : Reference<Document> { }
    internal class PositionReference : Reference<Position> 
    {
        public PositionReference() : base("Number")
        {

        }
    }
}
