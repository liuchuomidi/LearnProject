using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Services
{
   public class DialogService:IDialogService
    {
        public void ShowMessage(string message, string title = "提示")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool Confirm(string message, string title = "询问")
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
