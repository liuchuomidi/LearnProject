using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
   public class Book
    {
        private Guid id;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int pages;

        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
    }
}
