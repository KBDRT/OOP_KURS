using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Collections.Specialized;

namespace DocCreator
{
    // Справочники
    public abstract class Reference<T> : CloneSimple
    {
        protected ObservableCollection<T> Elements = new ObservableCollection<T>();

        private readonly Type ElementsType = typeof(T);
        private readonly PropertyInfo ID_MethodInfo;

        public void AddToList(T elem)
        {
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

        public void AddList(ObservableCollection<T> Elems)
        {
            Elements = Elems;
        }

        public void ClearList()
        {
            Elements.Clear();
        }

        public int GetElemCount()
        {
            return Elements.Count;
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
            foreach (T Elem in Elements)
            {
                i++;
                ID_MethodInfo?.SetValue(Elem, Convert.ToUInt16(i));
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    if (e.NewItems?[0] is T NewElem)
                        Changed(NewElem);
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    break;
            }
        }

        public virtual void Changed(T NewElement)
        {
            
        }

    }

    internal class CustomerReference : Reference<Customer> { }
    internal class TypeReference : Reference<TypeDocument> { }
    internal class BankReference : Reference<Bank> { }
    internal class ProductAndServiceReference : Reference<ProductAndService> { }
    internal class DocumentReference : Reference<Document> 
    {
        public void BubbleSortByTotalSumm(bool Desc)
        {
            for (int i = 1; i < Elements.Count; i++)
            {
                for (int j = 0; j < Elements.Count - i; j++)
                { 
                    if ((Desc && Elements[j].Positions.TotalSum > Elements[j + 1].Positions.TotalSum)
                        || (!Desc && Elements[j].Positions.TotalSum < Elements[j + 1].Positions.TotalSum))
                    {
                        SwapDocuments(j, j + 1);
                    }
                }
            }
        }

        private void SwapDocuments(int IndxFirst, int IndxSecond)
        {
            Document CopyFirst = Utils.Clone(Elements[IndxFirst]);
            Document CopySecond = Utils.Clone(Elements[IndxSecond]);
            Elements.RemoveAt(IndxFirst);
            Elements.Insert(IndxFirst, CopySecond);
            Elements.RemoveAt(IndxSecond);
            Elements.Insert(IndxSecond, CopyFirst);
        }
    }
    internal class OrganizationFormReference : Reference<OrganizationForm> { }
    public class PositionReference : Reference<Position> 
    {
        private float _TotalSum = 0;
        public float TotalSum { get => _TotalSum; set => SetValueField(ref _TotalSum, value); }

        public PositionReference() : base("Number")
        {

        }

        public override void Changed(Position NewElem)
        {
            NewElem.RecalcSum += RecalcTotal;
        }

        public void RecalcTotal(float OldValue, float NewValue)
        {
            TotalSum -= OldValue;
            TotalSum += NewValue;
        }
    }
}
