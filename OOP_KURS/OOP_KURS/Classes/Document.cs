using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Документ
    public class Document : CloneSimple
    {
        private ushort _Number;
        private Customer _Client;
        public TypeDocument _Type;
        public DateTime? _DocDate = DateTime.Now;
        private float TotalSum = 0;


        public ushort ID { get; set; }
        public ushort Number { get => _Number; set => SetValueField(ref _Number, value); }

        public TypeDocument Type { get => _Type; set => SetValueField(ref _Type, value); }

        public DateTime? DocDate { get => _DocDate; set => SetValueField(ref _DocDate, value); }

        public DateTime CreatedDate { set; get; }  = DateTime.Now;

        public Customer Client { get => _Client; set => SetValueField(ref _Client, value); }

        public ObservableCollection<Position> Positions { set; get; }  = new ObservableCollection<Position>();
    }
}
