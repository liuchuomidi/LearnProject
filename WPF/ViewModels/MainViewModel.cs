using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Events;
using WPF.InterF;
using WPF.Models;
using WPF.Services;

namespace WPF.ViewModels
{
    public class MainViewModel : ViewModelBase,IReloadData
    {
       public MainViewModel(IDialogService dialogservice)
       {
           AddBookCommand = new RelayCommand(add);
           EditBookCommand = new RelayCommand<Book>(edit, book=>book!=null);
           DeleteBookCommand = new RelayCommand<Book>(delete, book => book != null);
           AllBooks = new ObservableCollection<Book>(BookData.GetBook());
           Dialogservice = dialogservice;
       }

       private void delete(Book obj)
       {
           if (Dialogservice.Confirm("确认要删除嘛？"))
           {
               if (BookData.DeleteBook(obj))
               {
                   AllBooks.Remove(obj);
               }
           }
       }

       private void edit(Book obj)
       {
           Messenger.Default.Send(new EditBookArgs { Book = obj, IsEdit = true });
       }

       private void add()
       {
           Messenger.Default.Send(new EditBookArgs {IsEdit = false });
       }
        private ObservableCollection<Book> _allBooks;

        public ObservableCollection<Book> AllBooks
        {
            get { return _allBooks; }
            set { Set(ref _allBooks ,value); }
        }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { Set(ref selectedBook , value);
            EditBookCommand.RaiseCanExecuteChanged();
            DeleteBookCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand addBookCommand;

        public RelayCommand AddBookCommand
        {
            get { return addBookCommand; }
            set { addBookCommand = value; }
        }

        private RelayCommand<Book> editBookCommand;

        public RelayCommand<Book> EditBookCommand
        {
            get { return editBookCommand; }
            set { editBookCommand = value; }
        }

        public RelayCommand<Book> DeleteBookCommand { get; set; }

        public IDialogService Dialogservice { get; set; }

        public  void ReloadData() 
        {
            int index = AllBooks.IndexOf(SelectedBook);
            AllBooks = new ObservableCollection<Book>(BookData.GetBook());
            if (index >= 0) 
            {
                SelectedBook = AllBooks[index];
            }
        }
    }
}
