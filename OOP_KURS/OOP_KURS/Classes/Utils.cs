using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace OOP_KURS
{
    public abstract class CloneSimple : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    static class Utils
    {
        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
