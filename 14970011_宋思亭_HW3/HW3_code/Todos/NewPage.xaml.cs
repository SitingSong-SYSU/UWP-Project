using System;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewPage : Page
    {
        public NewPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        ViewModels.TodoItemViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
                this.ViewModel = e.Parameter as ViewModels.TodoItemViewModel;
                if (this.ViewModel.SelectedItem != null)
                {
                    createButton.Content = "Update";
                    title.Text = ViewModel.SelectedItem.title;
                    details.Text = ViewModel.SelectedItem.details;
                    date.Date = ViewModel.SelectedItem.date;
                    pic.Source = ViewModel.SelectedItem.img;
                    delete.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
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
                    this.ViewModel.AddItem(date.Date.DateTime, pic.Source, title.Text, details.Text);
                    CancelButton_Click(null, null);
                    ViewModel.SelectedItem = null;
                    await new MessageDialog("Create successfully!").ShowAsync();
                    Frame.Navigate(typeof(MainPage), "");
                }
                else
                {
                    this.ViewModel.SelectedItem.UpdateItem(date.Date.DateTime, pic.Source, title.Text, details.Text);
                    await new MessageDialog("Update successfully!").ShowAsync();
                    Frame.Navigate(typeof(MainPage), "");
                }
            }
        }
        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            details.Text = "";
            date.Date = System.DateTime.Now;
            RandomAccessStreamReference img = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/fruit.jpg"));
            IRandomAccessStream stream = await img.OpenReadAsync();
            Windows.UI.Xaml.Media.Imaging.BitmapImage bmp = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bmp.SetSource(stream);
            pic.Source = bmp;
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
            Frame.Navigate(typeof(MainPage), "");
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), "");
        }
    }
}
