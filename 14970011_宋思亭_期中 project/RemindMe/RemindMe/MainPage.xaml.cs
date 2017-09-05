using SQLitePCL;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using RemindMe.ViewModels;
using Windows.UI.Popups;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace RemindMe
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Colors.CornflowerBlue;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(800, 600));
        }

        public struct dateItems
        {
            public string date;
            public string title;
            public string finish;
        }

        //加载日历的时候把记录都添加上去
         private void CalendarView_CalendarViewDayItemChanging(CalendarView sender,
                                    CalendarViewDayItemChangingEventArgs args)
         {
            List<dateItems> allItems = new List<dateItems>();
            try
            {
                var sql = "SELECT date, title, finish FROM Todo WHERE date = ?";
                var conn = App.conn;
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, args.Item.Date.Date.ToString());
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        var date = statement[0].ToString();
                        var title = statement[1].ToString();
                        var finish = statement[2].ToString();
                        var tempItem = new dateItems();
                        tempItem.date = date;
                        tempItem.title = title;
                        tempItem.finish = finish;
                        allItems.Add(tempItem);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
            // Render basic day items.
            if (args.Phase == 0)
             {
                 // Register callback for next phase.
                 args.RegisterUpdateCallback(CalendarView_CalendarViewDayItemChanging);
             }
             // Set blackout dates.
             else if (args.Phase == 1)
             {
                 // Register callback for next phase.
                 args.RegisterUpdateCallback(CalendarView_CalendarViewDayItemChanging);
             }
             // Set density bars.
             else if (args.Phase == 2)
             {
                 //只允许在今后的日期添加新的备忘录
                 if (args.Item.Date > DateTimeOffset.Now)
                 {

                     List<Color> densityColors = new List<Color>();
                     foreach (var Item in allItems)
                     {
                        if (Item.finish == "true")
                        {
                            densityColors.Add(Colors.Green);
                        }
                        else
                        {
                            densityColors.Add(Colors.Blue);
                        }
                    }
                     args.Item.SetDensityColors(densityColors);
                 }
             }
         }

        private async void search_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var text = args.QueryText.Trim();
            if (text == "")
                return;
            string alert = "";
            try
            {
                var sql = "SELECT date, time, title, details FROM Todo WHERE date LIKE ? OR time LIKE ? OR title LIKE ? OR details LIKE ?";
                using (var statement = App.conn.Prepare(sql))
                {
                    statement.Bind(1, "%%" + text + "%%");
                    statement.Bind(2, "%%" + text + "%%");
                    statement.Bind(3, "%%" + text + "%%");
                    statement.Bind(4, "%%" + text + "%%");
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        var date = statement[0].ToString();
                        date = date.Substring(0, date.IndexOf(' '));
                        var time = statement[1].ToString();
                        string title = statement[2] as string;
                        string details = statement[3] as string;
                        alert += "Title: " + title + ";\nDetails: " + details + ";\nDue Time: " + date + "  " + time + "\n";
                    }
                    if (alert == "")
                        alert = "No result!\n";
                    await new MessageDialog(alert).ShowAsync();
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.ToString());
            }
        }

        private void Calendar_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            Common.selectDate = sender.SelectedDates[0].DateTime;
            Frame.Navigate(typeof(Todos));
        }
    }
}
