namespace DocCreator
{
    // Единица измерения
    public class Unit : CloneSimple
    {
        public ushort ID { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public ushort OKEI_CODE { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
