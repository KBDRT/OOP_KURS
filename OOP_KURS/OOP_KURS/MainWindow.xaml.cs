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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocumentForm DocView = new DocumentForm();
            DocView.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CustomerForm CustomerView = new CustomerForm();
            CustomerView.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BankForm View = new BankForm();
            View.Show();
        }

        private void Btn_Person_Click(object sender, RoutedEventArgs e)
        {
            DynamicForm View = new DynamicForm((sender as Button).Name);
            View.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TabView View = new TabView();
            View.Show();
        }
    }
}
