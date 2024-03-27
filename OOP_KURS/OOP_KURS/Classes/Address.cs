using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Адрес
    public class Address: CloneSimple
    {
        public int ID;
        public string Country { set; get; } = "Россия";
        public string Region { set; get; } = "Республика Татарстан";
        public string ShortRegion { get; }
        public string City { set; get; }
        public string Street { set; get; }
        public ushort House { set; get; }
        public ushort AppartNumber { set; get; }
        public string Complex { set; get; }
        public string Info { set; get; }
    }
}
