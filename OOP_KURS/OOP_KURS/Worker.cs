using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_KURS
{
    static class Worker
    {
        public static List<TypeDocument> Types = new List<TypeDocument>();

        public static List<Customer> Customers = new List<Customer>();

        public static void AddNewDocumentType(string im_Name, string im_Info = "")
        {
            Types.Add(new TypeDocument() { TypeCode = (uint)Types.Count + 1 , Name = im_Name, Info = im_Info }) ;
        }

        static Worker()
        {
            AddNewDocumentType("Счет");
            AddNewDocumentType("Акт выполненных работ");
            AddNewDocumentType("Товарная накладная");
        }
    }
}
