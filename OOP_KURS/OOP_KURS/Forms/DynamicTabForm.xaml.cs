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
        private readonly string RefName;
        public DynamicTabForm(string ReferenceName, string Mode = "Show")
        {
            InitializeComponent();

            RefName = ReferenceName;


            if (Mode != "Sel")
                LastRow.Height = new GridLength(0);

            FieldCatalog.SetColumnsForDataGrid(DataGrid, RefName);
            DataGrid.ItemsSource = ReferenceHelper.GetElementsByRefName(RefName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (RefName)
            {
                case "Customer":
                    CustomerForm CustomerView = new CustomerForm();
                    CustomerView.Show();
                    break;
                case "Address":
                    //AddressForm AddressForm = new AddressForm();
                    //AddressForm.Show();
                    break;
                case "Bank":
                    BankForm BankForm = new BankForm();
                    BankForm.Show();
                    break;
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count > 0)
            {
                List<dynamic> SelectedItemsList = DataGrid.SelectedItems.OfType<dynamic>().ToList();
                foreach (var chel in SelectedItemsList)
                {
                    ReferenceHelper.Delete(chel);
                }
            }
        }
    }
}
