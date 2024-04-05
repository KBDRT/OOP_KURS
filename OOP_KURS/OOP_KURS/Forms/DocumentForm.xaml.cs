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
using System.Windows.Shapes;

namespace OOP_KURS
{
    /// <summary>
    /// Логика взаимодействия для DocumentForm.xaml
    /// </summary>
    public partial class DocumentForm : Window
    {

        Document DocTemp = new Document();

        public DocumentForm()
        {
            InitializeComponent();

            popup1.StaysOpen = true;
          
            ComboBox_DocType.ItemsSource = ReferenceHelper.GetElementsByRefName("TypeDocument");
            ComboBox_DocType.DisplayMemberPath = "Name";

            if (ComboBox_DocType.Items.Count > 0)
                ComboBox_DocType.SelectedIndex = 0;

            ComboBox_Customer.ItemsSource = ReferenceHelper.GetElementsByRefName("Customer");
            ComboBox_Customer.DisplayMemberPath = "Name";

            FieldCatalog.SetColumnsForDataGrid(DataGrid_Pos, "Position");

            DataGrid_Pos.ItemsSource = DocTemp.Positions;

            //popup1.PlacementTarget = 

            //var chel = DataGrid_Pos.Columns[0].Name;


            //ComboBox_Customer.ItemsSource = Worker.Customers;
            //ComboBox_Customer.DisplayMemberPath = "Name";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocTemp.Positions.Add(new Position{Number = DocTemp.Positions.Count + 1 });
        }

        private void DataGrid_Pos_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //ListBox.Items.Add(new Label { Content = ""});

            //if (e.Key == Key.Enter)
            //{

            //ListBox.ItemsSource = null;

          

            var cell = e.OriginalSource as TextBox;
            if (cell != null)
            {
                string dataitem = cell.Text;  //Here you can you AS keyword to convert the DataContext to your item type.
                                              //dataitem.FirstName

                

                if (!String.IsNullOrEmpty(dataitem))
                {
                    ListBox.Items.Filter = f =>
                    {
                        string _text = (f as TypeDocument).Name;
                        return _text.StartsWith(dataitem, StringComparison.CurrentCultureIgnoreCase) || _text.IndexOf(dataitem, StringComparison.OrdinalIgnoreCase) >= 0;
                    };
                }

                //ListBox.DisplayMemberPath = "Name";

                //ListBox.Items.Add(new Label { Content = dataitem });
            }
            //..}


        }

        private void DataGrid_Pos_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            // popup1.PlacementTarget = sender AS;

            //ListBox.Items.Clear();

        



            var chel = (e.EditingEventArgs.Source as TextBlock);

            if (chel == null)
                return;


            ListBox.ItemsSource = null;
            ListBox.Items.Clear();
            popup1.IsOpen = false;


            ObservableCollection<TypeDocument> Elements;

            Elements = ReferenceHelper.GetElementsByRefName("TypeDocument");

            // Copy the list to the array.


            ListBox.ItemsSource = Elements;
            ListBox.DisplayMemberPath = "Name";

            ListBox.Items.Filter = f =>
            {
                string _text = (f as TypeDocument).Name;
                return true;
            };

            var chel2 = e.Column;

            popup1.Width = chel2.ActualWidth;
            popup1.PlacementTarget = chel;
            popup1.IsOpen = true;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TypeDocument> Elements;

            Elements = ReferenceHelper.GetElementsByRefName("TypeDocument");
        }
    }
}
