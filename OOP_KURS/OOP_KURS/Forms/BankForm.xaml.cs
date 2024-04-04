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
    /// Логика взаимодействия для BankForm.xaml
    /// </summary>
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
            Bank BankCopy = (Bank)Bank.Clone();

            ReferenceHelper.Add(BankCopy);

            //Utils.ClearPropertiesValue(Bank);

        }
    }
}
