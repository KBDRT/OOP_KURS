using System;
using System.Windows;

namespace DocCreator
{
    /// <summary>
    /// Логика взаимодействия для DynamicForm.xaml
    /// </summary>
    /// 

    public class Title
    { 
        public string ClassName;

        public string Value_1 { get; set; }
        public string Value_2 { get; set; }
        public string Value_3 { get; set; }
        public string Value_4 { get; set; }

        public string Text_1 { get; set; } = "Наименование товара/услуги";
        public string Text_2 { get; set; } = "Тип";
        public string Text_3 { get; set; } = "Единица измерения";
        public string Text_4 { get; set; } = "Дополнительная информация";
    }

    public partial class DynamicForm : Window
    {

        private Title FormWorker;

        public DynamicForm(string Name)
        {
            InitializeComponent();

            FormWorker = new Title{ ClassName = Name };

            DataContext = FormWorker;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReferenceHelper.Add(new ProductAndService
            {
                FullName = FormWorker.Value_1,
                Type = FormWorker.Value_2,
                //UnitOfMeasurement = Convert.ToByte(FormWorker.Value_3),
                Info = FormWorker.Value_4
            });
        }
    }
}
