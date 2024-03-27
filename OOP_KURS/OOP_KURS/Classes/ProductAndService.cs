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
        private uint Number;
        public decimal Amount { set; get; }
    }
}
