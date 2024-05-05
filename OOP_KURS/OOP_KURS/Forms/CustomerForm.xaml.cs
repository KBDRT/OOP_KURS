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

namespace DocCreator
{
    /// <summary>
    /// Логика взаимодействия для CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        public Customer Client = new Customer();
        public DynamicTabForm BankView = new DynamicTabForm("Bank", "Sel");
        private string FormMode;
        public CustomerForm(string Mode = "Client")
        {
            InitializeComponent();

            TextBox_PostalCode.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_INN.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_KPP.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_OGR.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_Pay.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            TextBox_Cor.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);

            Combobox_Form.ItemsSource = ReferenceHelper.GetElementsByRefName("OrganizationForm");
            Combobox_Form.DisplayMemberPath = "View";
            FormMode = Mode;

            if (Mode == "Client")
            {
                Client.Bank = BankView.BankForm.Bank;
                DataContext = Client;
            }
            else
            {
                Btn_Add.Visibility = Visibility.Hidden;
                BankView.BankForm.Bank = ReferenceHelper.Organization.Bank;
                DataContext = ReferenceHelper.Organization;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Customer Client2 = (Customer)Client.Clone();
            Client2.LegalAddress = (Address)Client.LegalAddress.Clone();
            Client2.CompanyRepresentative = (Person)Client.CompanyRepresentative.Clone();
            Client2.Bank = (Bank)Client.Bank.Clone();
            Client2.Form = (OrganizationForm)Client.Form.Clone();

            ReferenceHelper.Add(Client2);

            Utils.ClearPropertiesValue(Client);
            Client.LegalAddress.Country = "Россия";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BankView.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DynamicTabForm View = new DynamicTabForm("OrganizationForm");
            View.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FormMode == "Organization")
                ReferenceHelper.Organization.Bank = (Bank)BankView.BankForm.Bank?.Clone(); 
        }
    }
}
