using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Документ
    class Document : CloneSimple
    {
        public int ID;
        public uint Number { set; get; }
        public TypeDocument Type { set; get; }

        public DateTime dateTime = DateTime.Now;

        public ObservableCollection<Position> Positions = new ObservableCollection<Position>();
    }
}
