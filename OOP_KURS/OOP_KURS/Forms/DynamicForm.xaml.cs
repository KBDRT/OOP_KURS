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

        public void SetUnitText()
        {
            Text_1 = "Полное наименование";
            Text_2 = "Кратное наименование";
            Text_3 = "Код по ОКЕИ";
            Text_4 = "Дополнительная информация";
        }

    }

    public partial class DynamicForm : Window
    {

        private Title FormWorker;

        public DynamicForm(string Name)
        {
            InitializeComponent();

            FormWorker = new Title{ ClassName = Name };

            if (Name == "Unit")
                FormWorker.SetUnitText();

            DataContext = FormWorker;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FormWorker.ClassName == "Unit")
            {
                ReferenceHelper.Add(new Unit { FullName = FormWorker.Value_1,
                                               ShortName = FormWorker.Value_2,
                                               OKEI_CODE = Convert.ToUInt16(FormWorker.Value_3),
                                               AdditionalInfo = FormWorker.Value_4});
            }
            else
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
}
