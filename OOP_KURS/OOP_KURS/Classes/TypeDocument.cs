namespace DocCreator
{
    // Типы документов
    public class TypeDocument : CloneSimple
    {
        public ushort ID { get; set; }
        public string Name { set; get; }
        public string Info { set; get; }
    }
}
