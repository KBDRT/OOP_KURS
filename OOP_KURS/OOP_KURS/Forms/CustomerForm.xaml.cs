using System.Windows;
using System.Windows.Input;

namespace DocCreator
{
    public partial class CustomerForm : Window
    {
        public Customer Client = new Customer();
        public DynamicTabForm BankView ;
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
                DataContext = Client;
            }
            else
            {
                Btn_Add.Visibility = Visibility.Hidden;
                BankView = new DynamicTabForm("Bank", "Sel", null);
                BankView.BankForm.Bank = ReferenceHelper.Organization.Bank;
                DataContext = ReferenceHelper.Organization;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ReferenceHelper.Add(Utils.Clone(Client));
            Utils.ClearPropertiesValue(Client);
            Combobox_Form.SelectedIndex = -1;
            Client.LegalAddress.Country = "Россия";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BankView = new DynamicTabForm("Bank", "Sel", Client.Bank);
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
