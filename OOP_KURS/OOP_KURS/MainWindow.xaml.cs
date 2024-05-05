using MoreLinq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public MainWindow()
        {

           
            InitializeComponent();


            FieldCatalog.SetColumnsForDataGrid(DG, "Document");

            CollectionViewSource cvs = (CollectionViewSource)Resources["MyViewSource"];
            cvs.Source = ReferenceHelper.GetElementsByRefName("Document");


           // DG.ItemsSource = ReferenceHelper.GetElementsByRefName("Document");

            Customer Organization = new Customer();

            DG.Items.SortDescriptions.Clear();
            DG.Items.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Descending));
            DG.Items.Refresh();


            Organization.Bank.Name = "СБербанк";
            Organization.Bank.BIK = 123;
            Organization.CompanyRepresentative.FirstName = "Куршов";
            Organization.CompanyRepresentative.LastName = "иван";
            Organization.CompanyRepresentative.Patronymic = "директор";
            Organization.CorrespondentAccount = "12331234";
            Organization.Form.Name = "Общество";
            Organization.Form.ShortName = "ОО";
            Organization.INN = 123565;
            Organization.LegalAddress.City = "Челны";
            Organization.LegalAddress.AppartNumber = "23";
            Organization.LegalAddress.House = "9";
            Organization.LegalAddress.Region = "РТ";
            Organization.LegalAddress.Street = "Пушкина";
            Organization.Name = "ЗубОК";
            Organization.PaymentAccount = "12354";
            Organization.Phone = "89889";


            ReferenceHelper.Add(Organization);
            ReferenceHelper.Add(new Customer { Name = "test2",  KPP = 125235, INN = 233 });

            ListBox_1.DataContext = FilterItems;

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

        private void Open_Organization_Form(object sender, RoutedEventArgs e)
        {
            CustomerForm View = new CustomerForm("Organization");
            View.Show();
        }

        private DataGridColumnHeader chel;

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
                    chel = columnHeader;
                    PopupStart(columnHeader);
                }
            }

            base.OnPreviewMouseLeftButtonUp(e);
        }

        private void PopupStart(DataGridColumnHeader Header)
        {
            if (PopupFilter.IsOpen)
            {
                PopupFilter.IsOpen = false;
                chel.Style = new Style();
                return;
            }

            PopupFilter.IsOpen = false;
            FilterItems.Clear();

            ObservableCollection<Document> Elements = ReferenceHelper.GetElementsByRefName("Document");

            List<string> Name = Elements.DistinctBy(user => user.Client?.ID).ToList().Select(info => info.Client?.ViewName).ToList();

            foreach (string Elem in Name)
                FilterItems.Add(new ListBoxSelect { Text = Elem });

            // Header.BeginAnimation(Header.StylusButtonUp);

            Style buttonStyle = this.FindResource("StyleTest") as Style;
            //buttonStyle.Setters.Add(new Setter { Property = BasedOn });

            Header.Style = buttonStyle;

            if (Name.Count > 0)
            {
                ListBox_1.Items.Refresh();
                PopupFilter.Width = Header.ActualWidth;
                PopupFilter.PlacementTarget = DG;
                PopupFilter.IsOpen = true;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource cvs = (CollectionViewSource)Resources["MyViewSource"];

            cvs.View.Refresh();
        }

        private void yourFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Document item))
            {
                e.Accepted = false;
                return;
            }

            if (!FilterItems.Exists(x => x.IsSelected))
            {
                e.Accepted = true;
                return;
            }

            e.Accepted = FilterItems.Exists(x => x.IsSelected && x.Text == item.Client?.ViewName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopupFilter.IsOpen = false;
            chel.Style = new Style();
        }
    }
}
