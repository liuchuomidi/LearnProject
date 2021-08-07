using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Services;

namespace WPF.ViewModels
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditBookViewModel>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
        }


        private MainViewModel mainViewModel ;

        public MainViewModel MainViewModel
        {
            get { return mainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>(Guid.NewGuid().ToString()); }
            set { mainViewModel = value; }

        }

        private EditBookViewModel editBookViewModel ;

        public EditBookViewModel EditBookViewModel
        {
            get { return editBookViewModel = SimpleIoc.Default.GetInstance<EditBookViewModel>(Guid.NewGuid().ToString()); }
            set { editBookViewModel = value; }
        }
    }
}
