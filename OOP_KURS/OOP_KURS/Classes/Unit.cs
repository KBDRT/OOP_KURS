using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Единица измерения
    class Unit : CloneSimple
    {
        public int ID;
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public byte OKEI_CODE { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
