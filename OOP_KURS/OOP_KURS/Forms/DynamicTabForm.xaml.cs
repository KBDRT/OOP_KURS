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
using System.Collections;


namespace OOP_KURS
{
    /// <summary>
    /// Логика взаимодействия для DynamicTabForm.xaml
    /// </summary>
    public partial class DynamicTabForm : Window
    {
        private readonly string RefName;

        public CustomerForm CustomerView;
        public BankForm BankForm;

        public DynamicTabForm(string ReferenceName, string Mode = "Show")
        {


            InitializeComponent();


            RefName = ReferenceName;

            if (Mode != "Sel")
                LastRow.Height = new GridLength(0);

            BankForm = new BankForm();

            FieldCatalog.SetColumnsForDataGrid(DataGrid, RefName);

            DataGrid.ItemsSource = ReferenceHelper.GetElementsByRefName(RefName);

            FieldCatalog.SetPropertiesForWindow(MainWindow, RefName);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (RefName)
            {
                case "Customer":
                    CustomerView = new CustomerForm();
                    CustomerView.ShowDialog();
                    break;
                case "Unit":
                    DynamicForm View = new DynamicForm("Unit");
                    View.Show();
                    break;
                case "ProductAndService":
                    DynamicForm View_2 = new DynamicForm("ProductAndService");
                    View_2.Show();
                    break;
                case "Bank":
                    BankForm.Show();
                    break;
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count > 0)
            {
                List<dynamic> SelectedItemsList = DataGrid.SelectedItems.OfType<dynamic>().ToList();
                foreach (var Item in SelectedItemsList)
                {
                    ReferenceHelper.Delete(Item);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count > 0)
            {
                List<dynamic> SelectedItemsList = DataGrid.SelectedItems.OfType<dynamic>().ToList();
                foreach (Bank Item in SelectedItemsList)
                {
                    BankForm.Bank.Name = Item.Name;
                    BankForm.Bank.BIK = Item.BIK;
                    BankForm.Bank.GetView();
                }
            }

            this.Close();
        }
    }
}
