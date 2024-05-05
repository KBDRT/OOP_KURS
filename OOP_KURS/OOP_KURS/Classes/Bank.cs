namespace DocCreator
{
    // Банк
    public class Bank : CloneSimple
    {
        public ushort ID { get; set; }

        private string _Name;
        private int? _BIK;
        private string _ViewName;

        public string Name { get => _Name; set { SetValueField(ref _Name, value); GetView(); } }
        public int? BIK { get => _BIK; set { SetValueField(ref _BIK, value); GetView(); } }
        public string ViewName { get => _ViewName; set => SetValueField(ref _ViewName, value); }

        public void GetView()
        {
            ViewName = Name;
            if (BIK != null)
                ViewName += ", " + BIK;
        }
    }
}
