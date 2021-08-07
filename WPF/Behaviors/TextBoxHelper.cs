using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Behaviors
{
    public  class TextBoxHelper:DependencyObject
    {

        public static bool GetisOnlyNumber(DependencyObject obj)
        {
            return (bool)obj.GetValue(isOnlyNumberProperty);
        }

        public static void SetisOnlyNumber(DependencyObject obj, bool value)
        {
            obj.SetValue(isOnlyNumberProperty, value);
        }

        // Using a DependencyProperty as the backing store for isOnlyNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isOnlyNumberProperty =
            DependencyProperty.RegisterAttached("isOnlyNumber", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false,OnIsOnlyNumberChange));

        private static void OnIsOnlyNumberChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var text = d as TextBox;
            if ((bool)e.NewValue) 
            {
                text.PreviewTextInput += text_PreviewTextInput;
            }else{
                text.PreviewTextInput -= text_PreviewTextInput;
            }
        }

        static void text_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsNotNumber(e.Text);
        }

        private static bool IsNotNumber(string content) 
        {
            Regex regex = new Regex("[^0-9]");
            return regex.IsMatch(content);
        }
        
        
    }
}
