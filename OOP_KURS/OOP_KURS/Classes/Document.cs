using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    class Document
    {
        public uint ID { get; }
        public uint Number { set; get; }
        public TypeDocument Type { set; get; }

        public DateTime dateTime = DateTime.Now;

        public List<Position> Positions = new List<Position>();
    }
}
