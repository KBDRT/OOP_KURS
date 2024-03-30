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
        public string Name { get; set; }
        public int? BIK { get; set; }

        public string ViewName
        {
            get {
                string ret_val = Name;
                if (BIK != null)
                    ret_val += ", " + BIK;
                return ret_val; 
            }   
        }
    }
}
