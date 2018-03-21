using RemindMe.Models;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RemindMe.ViewModels
{
    class TodoItemViewModel
    {
        private ObservableCollection<TodoItem> items = new ObservableCollection<TodoItem>();
        private TodoItem selectedItem;

        public TodoItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
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
                        long ID = (long)statement[0];
                        var s = statement[1].ToString();
                        s = s.Substring(0, s.IndexOf(' '));
                        DateTime date = new DateTime(int.Parse(s.Split('/')[0]), int.Parse(s.Split('/')[1]), int.Parse(s.Split('/')[2]));
                        var t = statement[2].ToString();
                        TimeSpan time = new TimeSpan(int.Parse(t.Split(':')[0]), int.Parse(t.Split(':')[1]), int.Parse(t.Split(':')[2]));
                        string imgname = (string)statement[3];
                        string title = (string)statement[4];
                        string details = (string)statement[5];
                        bool finish = Boolean.Parse(statement[6] as string);
                        AddItem(ID, date, time, imgname, title, details, finish);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public void selectDateItems(DateTime selectDate)
        {
            getItems.Clear();
            try
            {
                var sql = "SELECT * FROM Todo WHERE date = ?";
                var conn = App.conn;
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, selectDate.Date.Date.ToString());
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        long ID = (long)statement[0];
                        var s = statement[1].ToString();
                        s = s.Substring(0, s.IndexOf(' '));
                        DateTime date = new DateTime(int.Parse(s.Split('/')[0]), int.Parse(s.Split('/')[1]), int.Parse(s.Split('/')[2]));
                        var t = statement[2].ToString();
                        TimeSpan time = new TimeSpan(int.Parse(t.Split(':')[0]), int.Parse(t.Split(':')[1]), int.Parse(t.Split(':')[2]));
                        string imgname = (string)statement[3];
                        string title = (string)statement[4];
                        string details = (string)statement[5];
                        bool finish = Boolean.Parse(statement[6] as string);
                        AddItem(ID, date, time, imgname, title, details, finish);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public ObservableCollection<TodoItem> getItems
        {
            get { return items; }
        }

        public void AddItem(long ID, DateTime date, TimeSpan time, string imgname, string title, string details, bool? finish)
        {
            items.Add(new TodoItem(ID, date, time, imgname, title, details, finish));
        }
    }
}
