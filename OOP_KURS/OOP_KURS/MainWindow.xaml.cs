using MoreLinq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DocCreator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<ListBoxSelect> FilterItems = new List<ListBoxSelect>();

        private DataGridColumnHeader PrevHeader;

        private char CurrentSort;

        public MainWindow()
        {
            InitializeComponent();


            FieldCatalog.SetColumnsForDataGrid(DG, "Document");

            CollectionViewSource cvs = (CollectionViewSource)Resources["MyViewSource"];
            cvs.Source = ReferenceHelper.GetElementsByRefName("Document");

            ListBox_1.DataContext = FilterItems;

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

        private void Open_Organization_Form(object sender, RoutedEventArgs e)
        {
            CustomerForm View = new CustomerForm("Organization");
            View.Show();
        }

        private void DG_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var obj = (DependencyObject)e.OriginalSource;

            while ((obj != null) && !(obj is DataGridColumnHeader))
                obj = VisualTreeHelper.GetParent(obj);

            if (obj == null)
                return;

            if (obj is DataGridColumnHeader columnHeader)
            {
                if (columnHeader.DisplayIndex == 2)
                {
                    PopupStart(columnHeader);
                    PrevHeader = columnHeader;
                }
                else if (columnHeader.DisplayIndex == 5)
                {
                    SortTotalSumColumn(columnHeader);
                }
            }

            base.OnPreviewMouseLeftButtonUp(e);
        }

        private void PopupStart(DataGridColumnHeader Header)
        {
            if (PopupFilter.IsOpen && PrevHeader == Header)
            {
                PopupFilter.IsOpen = false;
                Header.Style = new Style();
                return;
            }

            if (PrevHeader != null)
                PrevHeader.Style = new Style();

            PopupFilter.IsOpen = false;

            ObservableCollection<Document> Elements = ReferenceHelper.GetElementsByRefName("Document");

            List<string> Name = Elements.DistinctBy(user => user.Client?.ID).ToList().Select(info => info.Client?.ViewName).ToList();

            foreach (string Elem in Name)
            {
                if (!FilterItems.Exists(x => x.Text == Elem))
                    FilterItems.Add(new ListBoxSelect { Text = Elem });
            }

            Style buttonStyle = this.FindResource("StyleTest") as Style;

            if (Name.Count > 0)
            {
                ListBox_1.Items.Refresh();
                PopupFilter.Width = Header.ActualWidth;
                PopupFilter.PlacementTarget = Header;
               // Main.Width += PopupFilter.Width;
                Header.Style = buttonStyle;
                PopupFilter.IsOpen = true;
            }

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource cvs = (CollectionViewSource)Resources["MyViewSource"];

            cvs.View.Refresh();
        }

        private void FilterDocument(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Document item))
            {
                e.Accepted = false;
                return;
            }

            e.Accepted = true;

            if (FilterItems.Exists(x => x.IsSelected))
                e.Accepted = FilterItems.Exists(x => x.IsSelected && x.Text == item.Client?.ViewName);

            if (e.Accepted && !(bool)Check_Acc.IsChecked && item.Type?.ID == 1)
                e.Accepted = false;

            if (e.Accepted && !(bool)Check_Act.IsChecked && item.Type?.ID == 2)
                e.Accepted = false;
        }

        private void SortTotalSumColumn(DataGridColumnHeader Col)
        {
            if (CurrentSort == 'D')
            {
                Col.Content = Col.DataContext + " ▲";
                CurrentSort = 'A';
            }
            else
            {
                Col.Content = Col.DataContext + " ▼";
                CurrentSort = 'D';
            }
            ReferenceHelper.InvokeMethod("Document", "BubbleSortByTotalSumm", CurrentSort == 'A');
        }
    }
}
