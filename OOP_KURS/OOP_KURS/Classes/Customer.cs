using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Клиент
    public class Customer : CloneSimple
    {
        private string _Name;
        private string _Form;
        private string _Phone;
        private int? _INN;
        private int? _KPP;
        private string _OGR_Number;
        private string _PaymentAccount;
        private string _CorrespondentAccount;

        public ushort ID { get; set; }
        public string Name { get => _Name; set => SetValueField(ref _Name, value); }
        public string Form { get => _Form; set => SetValueField(ref _Form, value); }
        public Address LegalAddress { get; set; }
        public string Phone { get => _Phone; set => SetValueField(ref _Phone, value); }
        public int? INN { get => _INN; set => SetValueField(ref _INN, value); }
        public int? KPP { get => _KPP; set => SetValueField(ref _KPP, value); }
        public string OGR_Number { get => _OGR_Number; set => SetValueField(ref _OGR_Number, value); }
        public string PaymentAccount { get => _PaymentAccount; set => SetValueField(ref _PaymentAccount, value); }
        public string CorrespondentAccount { get => _CorrespondentAccount; set => SetValueField(ref _CorrespondentAccount, value); }
        public Bank Bank { get; set; }
        public Person CompanyRepresentative { get; set; }

        public string ViewName
        {
            get { return string.Format("{0} \"{1}\" ", Form, Name); }
        }

    }
}
