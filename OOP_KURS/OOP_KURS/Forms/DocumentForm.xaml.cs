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
    /// Логика взаимодействия для DocumentForm.xaml
    /// </summary>
    public partial class DocumentForm : Window
    {
        public DocumentForm()
        {
            InitializeComponent();

            ComboBox_DocType.ItemsSource = ReferenceHelper.GetElementsByRefName("TypeDocument");
            ComboBox_DocType.DisplayMemberPath = "Name";

            if (ComboBox_DocType.Items.Count > 0)
                ComboBox_DocType.SelectedIndex = 0;


            //ComboBox_Customer.ItemsSource = Worker.Customers;
            //ComboBox_Customer.DisplayMemberPath = "Name";
        }
    }
}
