using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    // Банк
    public class Bank : CloneSimple
    {
        public int ID;

        private string _Name;
        private int? _BIK;
        private string _ViewName;

        public string Name { get => _Name; set => SetValueField(ref _Name, value); }
        public int? BIK { get => _BIK; set => SetValueField(ref _BIK, value); }
        public string ViewName { get => _ViewName; set => SetValueField(ref _ViewName, value); }

        public void GetView()
        {
            ViewName = Name;
            if (BIK != null)
                ViewName += ", " + BIK;
        }
    }
}
