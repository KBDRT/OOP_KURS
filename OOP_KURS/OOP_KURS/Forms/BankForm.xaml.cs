using System.Windows;
using System.Windows.Input;

namespace DocCreator
{
    public partial class BankForm : Window
    {
        public Bank Bank;
        public BankForm()
        {
            Bank = new Bank();
            InitializeComponent();
            TextBox_BIK.PreviewTextInput += new TextCompositionEventHandler(Utils.NumberValidationTextBox);
            DataContext = Bank;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReferenceHelper.Add(Bank.Clone());
            Utils.ClearPropertiesValue(Bank);
        }
    }
}
