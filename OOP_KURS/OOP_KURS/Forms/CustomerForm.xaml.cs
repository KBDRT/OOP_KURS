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
        public Customer Client = new Customer();

        public DynamicTabForm BankView = new DynamicTabForm("Bank", "Sel");
        public CustomerForm()
        {
            InitializeComponent();

            // to:do
            TextBox_PostalCode.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_INN.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_KPP.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_OGR.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_Pay.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_Cor.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);

            Client.LegalAddress = new Address();
            Client.CompanyRepresentative = new Person();

            Client.Bank = BankView.BankForm.Bank;

            DataContext = Client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Customer Client2 = (Customer)Client.Clone();
            Client2.LegalAddress = (Address)Client.LegalAddress.Clone();
            Client2.CompanyRepresentative = (Person)Client.CompanyRepresentative.Clone();
            Client2.Bank = (Bank)Client.Bank.Clone();

            ReferenceHelper.Add(Client2);

            Utils.ClearPropertiesValue(Client);
            Client.LegalAddress.Country = "Россия";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BankView.Show();
        }
    }
}
