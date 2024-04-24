using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Типы документов
    public class TypeDocument : CloneSimple
    {
        public ushort ID { get; set; }
        public string Name { set; get; }
        public string Info { set; get; }
    }
}
