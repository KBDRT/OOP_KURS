using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOP_KURS
{
    static class Worker
    {
        public static List<TypeDocument> Types = new List<TypeDocument>();

        public static ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();

        static Dictionary<string, int> Counter = new Dictionary<string, int>
        {
            {"Customer", 0},
        };

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

        public static void AddNewCustomer(Customer im_Customer)
        {
            Counter["Customer"]++;
            im_Customer.ID = Counter["Customer"];
            Customers.Add((Customer)im_Customer.Clone());
        }

        public static void DeleteCustomer(List<Customer> Rows)
        {

        }
    }
}
