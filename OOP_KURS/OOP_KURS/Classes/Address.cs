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
        private int? _PostalCode;
        private string _Country = "Россия";
        private string  _Region;
        private string  _ShortRegion;
        private string  _City;
        private string  _Street;
        private string _House;
        private string _AppartNumber;
        private string  _Complex;
        private string  _Info;

        public ushort ID;
        public int? PostalCode { get => _PostalCode; set => SetValueField(ref _PostalCode, value); }
        public string Country { get => _Country; set => SetValueField(ref _Country, value); } 
        public string Region { get => _Region; set => SetValueField(ref _Region, value); }
        public string ShortRegion { get => _ShortRegion; set => SetValueField(ref _ShortRegion, value); }
        public string City { get => _City; set => SetValueField(ref _City, value); }
        public string Street { get => _Street; set => SetValueField(ref _Street, value); }
        public string House { get => _House; set => SetValueField(ref _House, value); }
        public string AppartNumber { get => _AppartNumber; set => SetValueField(ref _AppartNumber, value); }
        public string Complex { get => _Complex; set => SetValueField(ref _Complex, value); }
        public string Info { get => _Info; set => SetValueField(ref _Info, value); }

        public string FullName
        {
            get { return string.Format("{0} {1}", Country, Region); }
        }
    }
}
