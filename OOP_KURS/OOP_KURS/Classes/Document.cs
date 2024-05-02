using System;

namespace DocCreator
{
    // Документ
    public class Document : CloneSimple
    {
        private ushort _Number;
        private Customer _Client;
        public TypeDocument _Type;
        public DateTime? _DocDate = DateTime.Now;

        public ushort ID { get; set; }
        public ushort Number { get => _Number; set => SetValueField(ref _Number, value); }

        public TypeDocument Type { get => _Type; set => SetValueField(ref _Type, value); }

        public DateTime? DocDate { get => _DocDate; set => SetValueField(ref _DocDate, value); }

        public DateTime CreatedDate { set; get; }  = DateTime.Now;

        public Customer Client { get => _Client; set => SetValueField(ref _Client, value); }

        public PositionReference Positions { set; get; }  = new PositionReference();
    }
}
