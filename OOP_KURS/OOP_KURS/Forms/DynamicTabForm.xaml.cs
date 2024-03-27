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
using System.Configuration;
using System.Data;


namespace OOP_KURS
{
    /// <summary>
    /// Логика взаимодействия для DynamicTabForm.xaml
    /// </summary>
    public partial class DynamicTabForm : Window
    {
        public DynamicTabForm()
        {
            InitializeComponent();

            FieldCatalog.SetColumnsForDataGrid(DataGrid, "Customer");
            DataGrid.ItemsSource = ReferenceHelper.GetElementsByRefName("Customer");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerForm CustomerView = new CustomerForm();
            CustomerView.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count > 0)
            {
                //Type userType = Type.GetType("OOP_KURS.Customer");

                //Worker.DeleteCustomer(DataGrid.SelectedItems.OfType<Customer>().ToList());
            }
        }
    }
}
