using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.Events
{
    public class EditBookArgs
    {
        private bool isEdit;

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        private Book book;
        public Book Book
        {
            get { return book; }
            set { book = value; }
        }

}
}
