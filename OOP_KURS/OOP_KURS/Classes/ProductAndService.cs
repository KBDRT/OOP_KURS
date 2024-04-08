using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Товары и услуги
    class ProductAndService : CloneSimple
    {

        private string _FullName;

        public ushort ID;
        public string FullName { get => _FullName; set { SetValueField(ref _FullName, value); } }
        public string Type { set; get; }
        public Unit UnitOfMeasurement { set; get; }
        public string Info { set; get; }
    }

    class Position : ProductAndService
    {
        private float _Quantity;
        private float _Amount;
        private float _TotalAmount;

        public int Number { get; set; }
        public float Quantity { get => _Quantity; set { SetValueField(ref _Quantity, value); RecalcTotal(); } }
        public float Amount { get => _Amount; set { SetValueField(ref _Amount, value); RecalcTotal(); } }
        public float TotalAmount { get => _TotalAmount; set { SetValueField(ref _TotalAmount, value); } }

        private void RecalcTotal()
        {
            TotalAmount = Quantity * Amount;
        }

    }
}
