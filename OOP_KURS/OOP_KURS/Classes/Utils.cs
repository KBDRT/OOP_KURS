using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DocCreator
{
    public class CloneSimple : ICloneable, INotifyPropertyChanged
    {
        // Копирование значений
        public object Clone()
        {
            return MemberwiseClone();
        }

        // Реализация изменения поля
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void SetValueField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    public class ListBoxSelect : CloneSimple
    {
        private string _Text;

        private bool _isSelected;

        public bool IsSelected { get => _isSelected; set => SetValueField(ref _isSelected, value); }

        public string Text { get => _Text; set => SetValueField(ref _Text, value); }
    }

    static class Utils
    {
        // Ввод только числовых значений в TextBox
        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Очистить поля из объекта
        public static void ClearFieldsValue(object obj)
        {
            foreach (FieldInfo fi in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToArray())
            {
                fi.SetValue(obj, null);
            }
        }

        // Очистить свойства из объекта
        public static void ClearPropertiesValue(object obj)
        {
            foreach (PropertyInfo fi in obj?.GetType()?.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.ToArray())
            {
                if (!fi.CanWrite)
                    continue;

                if (fi.PropertyType.ToString().StartsWith("System."))
                {
                    fi.SetValue(obj, null);
                }
                else
                {
                    ClearPropertiesValue(fi.GetValue(obj)); // Глубокий тип, рекурсия
                }
            }
        }
    }

}
