using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Newtonsoft.Json;
using Todos.Models;
using Windows.UI.Text;
using Todos.ViewModels;

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TodoItemViewModel ViewModel = Common.ViewModel;
        public static string selectName = "";
        class myItem
        {
            public DateTimeOffset date;
            public string imgname;
            public string title;
            public string details;
            public bool? finish;
            public myItem(DateTimeOffset date, string imgname, string title, string details, bool? finish)
            {
                this.date = date;
                this.imgname = imgname;
                this.title = title;
                this.details = details;
                this.finish = finish;
            }
        }

        public static MainPage Current { get; internal set; }

        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (((App)Application.Current).IsSuspending)
            {
                ApplicationDataContainer Item = ApplicationData.Current.LocalSettings.CreateContainer("Item", ApplicationDataCreateDisposition.Always);
                if (ApplicationData.Current.LocalSettings.Containers.ContainsKey("Item"))
                {
                    Item.Values["imgname"] = selectName;
                    Item.Values["title"] = title.Text;
                    Item.Values["details"] = details.Text;
                    Item.Values["date"] = date.Date;
                    Item.Values["btn"] = createButton.Content;
                }
                if (ViewModel.SelectedItem != null)
                {
                    ApplicationData.Current.LocalSettings.Values["selectitem"] = ViewModel.getItems.IndexOf(ViewModel.SelectedItem);
                }
                List<string> L = new List<string>();
                var allitems = ViewModel.getItems;
                foreach (var a in allitems)
                {
                    var item = new myItem(a.date, a.imgname, a.title, a.details, a.finish);
                    L.Add(JsonConvert.SerializeObject(item));
                }
                ApplicationData.Current.LocalSettings.Values["allitems"] = JsonConvert.SerializeObject(L);
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            if (e.NavigationMode == NavigationMode.New)
            {
                ApplicationData.Current.LocalSettings.Values.Remove("Item");
                ApplicationData.Current.LocalSettings.Values.Remove("allitems");
                ApplicationData.Current.LocalSettings.Values.Remove("selectitem");
            }
            else
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("allitems"))
                {
                    ViewModel.getItems.Clear();
                    List<string> L = JsonConvert.DeserializeObject<List<string>>((string)ApplicationData.Current.LocalSettings.Values["allitems"]);
                    foreach (var l in L)
                    {
                        myItem a = JsonConvert.DeserializeObject<myItem>(l);
                        TodoItem item = new TodoItem(a.date.Date, a.imgname, a.title, a.details, a.finish);
                        ViewModel.getItems.Add(item);
                    }
                }
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("selectitem"))
                {
                    ViewModel.SelectedItem = ViewModel.getItems[(int)(ApplicationData.Current.LocalSettings.Values["selectitem"])];
                }

                if (ApplicationData.Current.LocalSettings.Containers.ContainsKey("Item"))
                {
                    ApplicationDataContainer Item = ApplicationData.Current.LocalSettings.Containers["Item"];
                    createButton.Content = Item.Values["btn"] as string;
                    title.Text = Item.Values["title"] as string;
                    details.Text = Item.Values["details"] as string;
                    date.Date = (DateTimeOffset)(Item.Values["date"]);
                    selectName = Item.Values["imgname"] as string;
                    if (selectName == "")
                    {
                        pic.Source = new BitmapImage(new Uri("ms-appx:///Assets/fruit.jpg"));
                    }
                    else
                    {
                        var file = await ApplicationData.Current.LocalFolder.GetFileAsync(selectName);
                        IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                        BitmapImage bitmapImage = new BitmapImage();
                        await bitmapImage.SetSourceAsync(fileStream);
                        pic.Source = bitmapImage;
                    }
                }
            }
        }

        private void itemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = e.ClickedItem as Models.TodoItem;
            if (InlineToDoItemViewGrid.Visibility.ToString() == "Collapsed")
            {
                Frame.Navigate(typeof(NewPage));
            }
            else
            {
                delete.Visibility = Visibility.Visible;
                createButton.Content = "Update";
                title.Text = ViewModel.SelectedItem.title;
                details.Text = ViewModel.SelectedItem.details;
                date.Date = ViewModel.SelectedItem.date;
                pic.Source = ViewModel.SelectedItem.img;
            }
        }

        private void checkBox(object sender, RoutedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);
            Line line = VisualTreeHelper.GetChild(parent, 3) as Line;
            line.Opacity = 1;
        }

        private void uncheckBox(object sender, RoutedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);
            Line line = VisualTreeHelper.GetChild(parent, 3) as Line;
            line.Opacity = 0;
        }

        private void btnNewPage(object sender, RoutedEventArgs e)
        {
            selectName = "";
            createButton.Content = "Create";
            CancelButton_Click(null, null);
            if (InlineToDoItemViewGrid.Visibility.ToString() == "Visible") return;
            Frame.Navigate(typeof(NewPage));
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            details.Text = "";
            date.Date = System.DateTime.Now;
            RandomAccessStreamReference img = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/fruit.jpg"));
            IRandomAccessStream stream = await img.OpenReadAsync();
            BitmapImage bmp = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bmp.SetSource(stream);
            pic.Source = bmp;
        }

        private async void CreatButton_Click(object sender, RoutedEventArgs e)
        {
            if (title.Text == "")
            {
                var i = new MessageDialog("Title can not be empty!").ShowAsync();
            }
            if (details.Text == "")
            {
                var i = new MessageDialog("Detail can not be empty!").ShowAsync();
            }
            if (date.Date.CompareTo(DateTime.Today) < 0)
            {
                var i = new MessageDialog("The due date has passed!").ShowAsync();
            }
            if (title.Text != "" && details.Text != "" && date.Date.CompareTo(DateTime.Today) >= 0)
            {
                if (createButton.Content.ToString() == "Create")
                {
                    this.ViewModel.AddItem(date.Date.DateTime, selectName, title.Text, details.Text);
                    CancelButton_Click(null, null);
                    ViewModel.SelectedItem = null;
                    await new MessageDialog("Create successfully!").ShowAsync();
                }
                else
                {
                    this.ViewModel.SelectedItem.UpdateItem(date.Date.DateTime, selectName, title.Text, details.Text);
                    await new MessageDialog("Update successfully!").ShowAsync();
                }
            }
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Pickers.FileOpenPicker newphoto = new FileOpenPicker();
            newphoto.FileTypeFilter.Add(".jpg");
            newphoto.FileTypeFilter.Add(".jpeg");
            newphoto.FileTypeFilter.Add(".png");
            newphoto.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            StorageFile file = await newphoto.PickSingleFileAsync();
            if (file != null)
            {
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                Windows.UI.Xaml.Media.Imaging.BitmapImage bmp = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                bmp.SetSource(stream);
                this.pic.Source = bmp;
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            delete.Visibility = Visibility.Collapsed;
            createButton.Content = "Create";
            CancelButton_Click(null, null);
            ViewModel.getItems.Remove(ViewModel.SelectedItem);
            ViewModel.SelectedItem = null;
        }
    }
}