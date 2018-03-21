using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Todos.ViewModels;
using Windows.UI.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Todos.Models;

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewPage : Page
    {
        private TodoItemViewModel ViewModel = Common.ViewModel;
        private string selectName = Common.selectName;
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

        public NewPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
        }
    
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // 保存数据
            if (((App)Application.Current).IsSuspending)
            {
                ApplicationDataContainer Item = ApplicationData.Current.LocalSettings.CreateContainer("Item", ApplicationDataCreateDisposition.Always);
                if (ApplicationData.Current.LocalSettings.Containers.ContainsKey("Item"))
                {
                    Item.Values["title"] = title.Text;
                    Item.Values["details"] = details.Text;
                    Item.Values["date"] = date.Date;
                    Item.Values["imgname"] = Common.selectName;
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
            // 尝试恢复
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            if (e.NavigationMode == NavigationMode.New)
            {
                if (ViewModel.SelectedItem != null)
                {
                    createButton.Content = "Update";
                    title.Text = ViewModel.SelectedItem.title;
                    details.Text = ViewModel.SelectedItem.details;
                    date.Date = ViewModel.SelectedItem.date;
                    pic.Source = ViewModel.SelectedItem.img;
                }
                ApplicationData.Current.LocalSettings.Values.Remove("Item");
                ApplicationData.Current.LocalSettings.Values.Remove("allitems");
                ApplicationData.Current.LocalSettings.Values.Remove("selectitem");
            }
            else
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("allitems"))
                {
                    var allitems = ViewModel.getItems;
                    allitems.Clear();
                    List<string> L = JsonConvert.DeserializeObject<List<string>>(
                      (string)ApplicationData.Current.LocalSettings.Values["allitems"]);
                    foreach (var l in L)
                    {
                        myItem a = JsonConvert.DeserializeObject<myItem>(l);
                        TodoItem item = new TodoItem(a.date.Date, a.imgname, a.title, a.details, a.finish);
                        allitems.Add(item);
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
                    Common.selectName = Item.Values["imgname"] as string;

                    if (Common.selectName == "")
                    {
                        pic.Source = new BitmapImage(new Uri("ms-appx:///Assets/fruit.jpg"));
                    }
                    else
                    {
                        var file = await ApplicationData.Current.LocalFolder.GetFileAsync(Common.selectName);
                        IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                        BitmapImage bitmapImage = new BitmapImage();
                        await bitmapImage.SetSourceAsync(fileStream);
                        pic.Source = bitmapImage;
                    }
                }
            }
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
                    this.ViewModel.AddItem(date.Date.DateTime, Common.selectName, title.Text, details.Text);
                    Common.selectName = "";
                    CancelButton_Click(null, null);
                    ViewModel.SelectedItem = null;
                    await new MessageDialog("Create successfully!").ShowAsync();
                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    this.ViewModel.SelectedItem.UpdateItem(date.Date.DateTime, Common.selectName, title.Text, details.Text);
                    Common.selectName = "";
                    await new MessageDialog("Update successfully!").ShowAsync();
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            details.Text = "";
            selectName = "";
            date.Date = System.DateTime.Now;
            RandomAccessStreamReference img = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/fruit.jpg"));
            IRandomAccessStream stream = await img.OpenReadAsync();
            Windows.UI.Xaml.Media.Imaging.BitmapImage bmp = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bmp.SetSource(stream);
            pic.Source = bmp;
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            var fop = new FileOpenPicker();
            fop.ViewMode = PickerViewMode.Thumbnail;
            fop.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fop.FileTypeFilter.Add(".jpg");
            fop.FileTypeFilter.Add(".jpeg");
            fop.FileTypeFilter.Add(".png");
            fop.FileTypeFilter.Add(".gif");

            StorageFile file = await fop.PickSingleFileAsync();
            try
            {
                IRandomAccessStream fileStream;
                using (fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);
                    pic.Source = bitmapImage;
                    var name = file.Path.Substring(file.Path.LastIndexOf('\\') + 1);
                    Common.selectName = name;
                    await file.CopyAsync(ApplicationData.Current.LocalFolder, name, NameCollisionOption.ReplaceExisting);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return;
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            selectName = "";
            delete.Visibility = Visibility.Collapsed;
            createButton.Content = "Create";
            CancelButton_Click(null, null);
            ViewModel.getItems.Remove(ViewModel.SelectedItem);
            ViewModel.SelectedItem = null;
            Frame.Navigate(typeof(MainPage));
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            selectName = "";
            Frame.Navigate(typeof(MainPage));
        }
    }
}
