namespace DocCreator
{
    public class OrganizationForm : CloneSimple
    {
        private string _Name;
        private string _ShortName;

        public ushort ID { get; set; }

        public string Name { get => _Name; set => SetValueField(ref _Name, value); }
        public string ShortName { get => _ShortName; set => SetValueField(ref _ShortName, value); }

        public string View { get { return ShortName + " " + Name; } }
    }
}
