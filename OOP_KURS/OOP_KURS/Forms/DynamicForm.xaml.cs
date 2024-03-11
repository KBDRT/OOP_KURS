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
        public string Text_1 { get; set; } = "Наименование товара/услуги";
        public string Text_2 { get; set; } = "Тип";
        public string Text_3 { get; set; } = "Единица измерения";
        public string Text_4 { get; set; } = "Дополнительная информация";

        public void SetPersonText(Button btn)
        {
            Text_1 = "Фамилия";
            Text_2 = "Имя";
            Text_3 = "Отчество";
            Text_4 = "Должность";

            HideBtn(btn);
        }
        public void SetUnitText(Button btn)
        {
            Text_1 = "Полное наименование";
            Text_2 = "Кратное наименование";
            Text_3 = "Код по ОКЕИ";
            Text_4 = "Дополнительная информация";

            HideBtn(btn);
        }

        private void HideBtn(Button btn)
        {
            btn.Visibility = Visibility.Collapsed;
        }
    }

    public partial class DynamicForm : Window
    {

        public Title Text { get; set; }
        public DynamicForm(string BtnName)
        {
            InitializeComponent();

            Title Text = new Title();

            if (BtnName.Substring(4) == "Person")
                Text.SetPersonText(Btn_1);
            else if (BtnName.Substring(4) == "Unit")
                Text.SetUnitText(Btn_1);

            DataContext = Text;
        }
    }
}
