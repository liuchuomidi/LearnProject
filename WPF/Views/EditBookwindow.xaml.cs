using GalaSoft.MvvmLight.Messaging;
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
using WPF.Events;

namespace WPF.Views
{
    /// <summary>
    /// EditBookwindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditBookwindow : Window
    {
        public EditBookwindow()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseWindowArgs>(this, CloseChange);
            this.Unloaded += EditBookwindow_Unloaded;
        }

        void EditBookwindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<CloseWindowArgs>(this, CloseChange);
        }

        private void CloseChange(CloseWindowArgs obj)
        {
           this.DialogResult = obj.DialogResult;
            //this.Close();
        }
    }
}
