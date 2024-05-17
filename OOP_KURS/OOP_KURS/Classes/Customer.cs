namespace DocCreator
{
    // Клиент
    public class Customer : CloneSimple
    {
        private string _Name;
        private string _Phone;
        private ulong? _INN;
        private int? _KPP;
        private string _OGR_Number;
        private string _PaymentAccount;
        private string _CorrespondentAccount;
        private string _ViewName;

        public ushort ID { get; set; }
        public string Name { get => _Name; set { SetValueField(ref _Name, value); ViewName = ""; } }
        public OrganizationForm Form { get; set; } = new OrganizationForm();
        public Address LegalAddress { get; set; } = new Address();
        public string Phone { get => _Phone; set => SetValueField(ref _Phone, value); }
        public ulong? INN { get => _INN; set => SetValueField(ref _INN, value); }
        public int? KPP { get => _KPP; set => SetValueField(ref _KPP, value); }
        public string OGR_Number { get => _OGR_Number; set => SetValueField(ref _OGR_Number, value); }
        public string PaymentAccount { get => _PaymentAccount; set => SetValueField(ref _PaymentAccount, value); }
        public string CorrespondentAccount { get => _CorrespondentAccount; set => SetValueField(ref _CorrespondentAccount, value); }
        public Bank Bank { get; set; } = new Bank();
        public Person CompanyRepresentative { get; set; } = new Person();
        public string ViewName { get => _ViewName; set { SetValueField(ref _ViewName, string.Format("{0} \"{1}\" ", Form?.ShortName, Name)); } }
    }
}
