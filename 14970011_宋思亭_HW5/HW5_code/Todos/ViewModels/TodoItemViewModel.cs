using System;
using System.Collections.ObjectModel;
using Todos.Models;

namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        private ObservableCollection<Models.TodoItem> items = new ObservableCollection<Models.TodoItem>();
        private TodoItem selectedItem;

        public TodoItem SelectedItem
        {
            get { return selectedItem; }
            set { this.selectedItem = value; }
        }

        public TodoItemViewModel()
        {
            this.SelectedItem = null;
            this.items.Add(new Models.TodoItem(DateTime.Today, "", "完成作业", "123", true));
            this.items.Add(new Models.TodoItem(DateTime.Today, "", "复习", "456"));
        }

        public ObservableCollection<Models.TodoItem> getItems
        {
            get { return this.items; }
        }

        public void AddItem(DateTime date, string imgname, string title, string detail)
        {
            items.Add(new Models.TodoItem(date, imgname, title, detail));
        }
    }
}
