using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DocCreator
{
    /// <summary>
    /// Логика взаимодействия для DynamicTabForm.xaml
    /// </summary>
    public partial class DynamicTabForm : Window
    {
        private readonly string RefName;

        public CustomerForm CustomerView;
        public BankForm BankForm;
        private Bank CurrentBank;

        public DynamicTabForm(string ReferenceName, string Mode = "Show", Bank SelBank = null)
        {

            InitializeComponent();

            RefName = ReferenceName;

            if (Mode != "Sel")
            {
                LastRow.Height = new GridLength(0);
                Btn_Add.Visibility = Visibility.Hidden;
            }
            else
            {
                CurrentBank = SelBank;
            }

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
                case "ProductAndService":
                    DynamicForm View_2 = new DynamicForm("ProductAndService");
                    View_2.Show();
                    break;
                case "Bank":
                    BankForm = new BankForm();
                    BankForm.Show();
                    break;
                case "OrganizationForm":
                    ReferenceHelper.Add(new OrganizationForm { });
                    break;
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count > 0)
            {
                List<dynamic> SelectedItemsList = DataGrid.SelectedItems.OfType<dynamic>().ToList();
                foreach (dynamic Item in SelectedItemsList)
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
                    CurrentBank.Name = Item.Name;
                    CurrentBank.BIK = Item.BIK;
                    CurrentBank.GetView();
                }
            }

            Close();
        }

        private void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }
    }
}
