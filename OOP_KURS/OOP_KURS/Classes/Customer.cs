using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    public class Customer : ICloneable
    {
        private string ID;
        public string Name { get; set; }
        public string Form { get; set; }
        public Address LegalAddress { get; set; }
        public string Phone { get; set; }
        public int INN { get; set; }
        public int KPP { get; set; }
        public string OGR_Number { get; set; }
        public string PaymentAccount { get; set; }
        public string CorrespondentAccount { get; set; }
        public Bank Bank { get; set; }
        public Person CompanyRepresentative { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
