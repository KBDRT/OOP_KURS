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
        public int ID;
        public string FullName { set; get; }
        public string Type { set; get; }
        public Unit UnitOfMeasurement { set; get; }
        public string Info { set; get; }
    }

    class Position : ProductAndService
    {
        private decimal _Quantity;
        private decimal _Amount;
        private decimal _TotalAmount;

        public int Number { get; set; }
        public decimal Quantity { get => _Quantity; set { SetValueField(ref _Quantity, value); RecalcTotal(); } }
        public decimal Amount { get => _Amount; set { SetValueField(ref _Amount, value); RecalcTotal(); } }
        public decimal TotalAmount { get => _TotalAmount; set { SetValueField(ref _TotalAmount, value); } }

        private void RecalcTotal()
        {
            TotalAmount = Quantity * Amount;
        }

    }
}
