using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MySelf.QOSM.QuickOrderAPP.Utils
{
    public class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata("", OnPasswordPropertyChanged));
        public static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)d;
            string password = (string) e.NewValue;
            if (passwordBox != null && passwordBox.Password != password)
            {
                passwordBox.Password = password;
            }
        }
        public static string GetPassword(DependencyObject o)
        {
            return (string)o.GetValue(PasswordProperty);
        }
        public static void SetPassword(DependencyObject o, string value)
        {
            o.SetValue(PasswordProperty, value);
        }
    }
}
