using System;

namespace DocCreator
{
    // Предствитель компании
    public class Person : CloneSimple
    {
        private string _LastName;
        private string _FirstName;
        private string _Patronymic;
        private string _Position;

        public ushort ID { get; set; }
        public string LastName { get => _LastName; set => SetValueField(ref _LastName, value); }
        public string FirstName { get => _FirstName; set => SetValueField(ref _FirstName, value); }
        public string Patronymic { get => _Patronymic; set => SetValueField(ref _Patronymic, value); }
        public string Position { get => _Position; set => SetValueField(ref _Position, value); }
        public string ViewName
        {
            get {
                string ret_val = "";

                if (!String.IsNullOrEmpty(FirstName))
                    ret_val = LastName + " " + FirstName[0] + ".";

                if (!String.IsNullOrEmpty(_Patronymic))
                    ret_val += Patronymic[0] + ".";

                if (!String.IsNullOrEmpty(_Position))
                    ret_val += ", " + Position;

                return ret_val;
            }
        }

    }
}

