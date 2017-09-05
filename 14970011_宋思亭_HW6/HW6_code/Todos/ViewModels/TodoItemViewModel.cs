using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            try
            {
                var sql = "SELECT * FROM Todo";
                var conn = App.conn;
                using (var statement = conn.Prepare(sql))
                {
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        var s = statement[1].ToString();
                        s = s.Substring(0, s.IndexOf(' '));
                        long ID = (long)statement[0];
                        DateTime date = new DateTime(int.Parse(s.Split('/')[0]), int.Parse(s.Split('/')[1]), int.Parse(s.Split('/')[2]));
                        string imgname = (string)statement[2];
                        string title = (string)statement[3];
                        string details = (string)statement[4];
                        bool finish = Boolean.Parse(statement[5] as string);
                        this.AddItem(ID, date, imgname, title, details, finish);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public ObservableCollection<Models.TodoItem> getItems
        {
            get { return this.items; }
        }

        public void AddItem(long ID, DateTime date, string imgname, string title, string details, bool? finish)
        {
            items.Add(new Models.TodoItem(ID, date, imgname, title, details, finish));
        }
    }
}
