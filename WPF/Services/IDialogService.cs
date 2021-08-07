using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Services
{
   public interface IDialogService
    {
       void ShowMessage(string message, string title = "提示");
       bool Confirm(string message, string title = "询问");
    }
}
