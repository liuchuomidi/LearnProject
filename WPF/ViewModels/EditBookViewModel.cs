using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Events;
using WPF.InterF;
using WPF.Models;
using WPF.Services;

namespace WPF.ViewModels
{
    public class EditBookViewModel : ViewModelBase, IeditBook
    {
        public EditBookViewModel(IDialogService dialogService)
        {
           // _currentBook = new Book();
            Viewload = new RelayCommand(viewl);
            SaveCommand = new RelayCommand(save);
            BackCommand = new RelayCommand(back);
            DialogService = dialogService;
        }

        private void back()
        {
            Messenger.Default.Send(new CloseWindowArgs { DialogResult = false });
        }

        private void save()
        {
            bool result = false;
            if (EditBookArgs.IsEdit)
            {
              result=  BookData.UpdateBook(CurrentBook);
            }
            else 
            {
               result= BookData.AddBook(CurrentBook);
            }

            if (result) 
            {
                //todo
                DialogService.ShowMessage(EditBookArgs.IsEdit ? "编辑成功" : "新增成功");
                Messenger.Default.Send(new CloseWindowArgs { DialogResult = true });
            }
        }

        private void viewl()
        {
            if (EditBookArgs.IsEdit)
            {
                CurrentBook = new Book
                {
                    Id = EditBookArgs.Book.Id,
                    Author = EditBookArgs.Book.Author,
                    Description = EditBookArgs.Book.Description,
                    Pages = EditBookArgs.Book.Pages,
                    Title = EditBookArgs.Book.Title
                };
            }
            else 
            {
                CurrentBook = new Book { Id = Guid.NewGuid() };
            }
            WindowTitle = EditBookArgs.IsEdit ? "编辑" : "新增";
        }
        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { Set(ref _windowTitle ,value); }
        }
        private Book _currentBook;

        public Book CurrentBook
        {
            get { return _currentBook; }
            set { Set( ref _currentBook , value); }
        }

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }
        private RelayCommand backCommand;

        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }

        private RelayCommand viewload;

        public RelayCommand Viewload
        {
            get { return viewload; }
            set { viewload = value; }
        }

        private IDialogService dialogService;

        public IDialogService DialogService
        {
            get { return dialogService; }
            set { dialogService = value; }
        }


        public EditBookArgs EditBookArgs
        {
            get;
            set;
        }
    }
}
