using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Документ
    public class Document : CloneSimple
    {
        public ushort ID { get; set; }
        public ushort Number { set; get; }
        public TypeDocument Type { set; get; }

        public DateTime? DocDate { set; get; }  = DateTime.Now;

        public DateTime CreatedDate { set; get; }  = DateTime.Now;

        public Customer Client { set; get; }  = new Customer();

        public ObservableCollection<Position> Positions { set; get; }  = new ObservableCollection<Position>();
    }
}
