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
using WPF.InterF;

namespace WPF.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<EditBookArgs>(this, Onmake);
            this.Unloaded += MainWindow_Unloaded;
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<EditBookArgs>(this, Onmake);
        }

        private void Onmake(EditBookArgs obj)
        {
            EditBookwindow window = new EditBookwindow();
            (window.DataContext as IeditBook).EditBookArgs = obj;
           var result= window.ShowDialog();
           if (result.HasValue && result.Value) 
           {
               //todo
               (this.DataContext as IReloadData).ReloadData();
           }
        }
    }
}
