using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocCreator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            

            InitializeComponent();


            FieldCatalog.SetColumnsForDataGrid(DG, "Document");

            DG.ItemsSource = ReferenceHelper.GetElementsByRefName("Document");

            ReferenceHelper.Add(new Customer { Name = "test" });
            ReferenceHelper.Add(new Customer { Name = "test2" });

            //ReferenceHelper.InvokeMethod("Customer", "ClearList");

            // Test.RemoveAt(0);

            //var MyObservableCollection = ReferenceHelper.GetElementsByRefName("Customer");

            // DG.DataContext = MyObservableCollection;

            // DG.ItemsSource = ReferenceHelper.GetElementsByRefName("Customer");

            //ReferenceHelper.Add(new Customer { Name = "A" });

            //FieldCatalog.SetColumnsForDataGrid(DG, "Document");
        }

        private void Open_Reference_Form(object sender, RoutedEventArgs e)
        {
            if ((sender as MenuItem).Name.Length != 0)
            {
                DynamicTabForm View = new DynamicTabForm((sender as MenuItem).Name.Substring(4));
                View.Show();
            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DocumentForm View = new DocumentForm();
            View.Show();
        }

        private void DG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid Grid = sender as DataGrid;
                if (Grid != null && Grid.SelectedItems != null && Grid.SelectedItems.Count == 1)
                {
                    DataGridRow Row = Grid.ItemContainerGenerator.ContainerFromItem(Grid.SelectedItem) as DataGridRow;

                    DocumentForm Form = new DocumentForm("Edit", (Document)Row.DataContext);
                    Form.ShowDialog();

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double text = Convert.ToDouble(Test.Text);
            AmountConverter.Convert(text, out string Kop);
        }
    }
}
