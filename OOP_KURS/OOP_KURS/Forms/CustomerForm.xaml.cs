using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOP_KURS
{
    /// <summary>
    /// Логика взаимодействия для CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        Customer Client = new Customer();

        int chel = 1;
        public CustomerForm()
        {
            InitializeComponent();
            Client.LegalAddress = new Address();
            Client.CompanyRepresentative = new Person();
            DataContext = Client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Bank bnk = new Bank() { Name = "ВТБ", ID = chel };
            chel++;

            Client.Bank = bnk;

            Customer Client2 = (Customer)Client.Clone();

            ReferenceHelper.Add(Client2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DynamicTabForm AddressForm = new DynamicTabForm("Address", "Sel");
            AddressForm.Show();
        }
    }
}
