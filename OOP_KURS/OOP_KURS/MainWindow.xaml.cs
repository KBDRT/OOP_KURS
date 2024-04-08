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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP_KURS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ListBox.Items.Add("test");


            //PopupSuggest.PlacementTarget = Doc;

            DG.ItemsSource = ReferenceHelper.GetElementsByRefName("Document");
            FieldCatalog.SetColumnsForDataGrid(DG, "Document");


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

    }
}
