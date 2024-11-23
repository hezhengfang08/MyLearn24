using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Myself.SmartParking.Base
{
    public class PasswordEx
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
            "Password", typeof(string), typeof(PasswordEx), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChange)));

        private static void OnPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pd = (PasswordBox)d;
            pd.PasswordChanged -= Pb_PasswordChange;
            if(!isUpdating)
                pd.Password = e.NewValue.ToString();
            pd.PasswordChanged += Pb_PasswordChange;

        }
        static bool isUpdating = false;
        private static void Pb_PasswordChange(object sender, RoutedEventArgs e)
        {
            PasswordBox ps = (PasswordBox)sender;
            isUpdating = true;
            SetPassword(ps,ps.Password);
            isUpdating = false;
        }
        public static void SetPassword(DependencyObject d, string password)
        {
            d.SetValue(PasswordProperty, password);
        }
    }
}
