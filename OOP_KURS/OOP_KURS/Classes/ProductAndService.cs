using System;

namespace DocCreator
{
    // Товары и услуги
    public class ProductAndService : CloneSimple
    {

        private string _FullName;

        public ushort ID;
        public string FullName { get => _FullName; set => SetValueField(ref _FullName, value); }
        public string Type { set; get; }
        public Unit UnitOfMeasurement { set; get; }
        public string Info { set; get; }
    }

    public class Position : ProductAndService
    {
        private int _Number;
        private float _Quantity;
        private float _Amount;
        private float _TotalAmount;

        public int Number { get => _Number; set => SetValueField(ref _Number, value); }
        public float Quantity
        {
            get => _Quantity;
            set
            {
                float val = (float)Math.Round(value, 2);
                SetValueField(ref _Quantity, val);
                RecalcTotal();
            }
        }
        public float Amount
        { 
            get => _Amount;
            set
            {
                float val = (float)Math.Round(value, 2);
                SetValueField(ref _Amount, val);
                RecalcTotal();
            }
        }
        public float TotalAmount { get => _TotalAmount; set => SetValueField(ref _TotalAmount, value); }

        public delegate void TotalAmountChanged(float OldSumm, float newSumm);
        public event TotalAmountChanged RecalcSum;

        private void RecalcTotal()
        {
            float NewSum = (float)Math.Round(Quantity * Amount, 2);
            RecalcSum?.Invoke(TotalAmount, NewSum);
            TotalAmount = NewSum;
        }

    }
}
