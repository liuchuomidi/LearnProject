using System;
using System.Collections.Generic;
using System.Text;
using LearnProject.ViewModel;
using LearnProject.Commond;
namespace LearnProject.ViewModel
{
    class MainWindowViewModel:ViewModeBase
    {
        private double input1;
        private double input2;
        private double result;
        public double Input1
        {
            get { return input1; }
            set
            {
                Set(ref input1, value, nameof(Input1));
            }
        }
        public double Input2
        {
            get { return input2; }
            set
            {
                Set(ref input2, value,nameof(Input2));
            }
        }

        public double Result
        {
            get { return result; }
            set
            {
                Set(ref result, value,nameof(Result));
            }
        }
        public DeleteCommand AddCommand { get; set; }


        public void add(object paramater)
        {
            Result = Input1 + Input2;
        }

        public MainWindowViewModel()
        {
            
            AddCommand=new DeleteCommand();
            AddCommand.excute = add;
        }
    }
}
