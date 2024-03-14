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
        public CustomerForm()
        {
            InitializeComponent();
            DataContext = Client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Worker.AddNewCustomer(Client);
        }
    }
}
