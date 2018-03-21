using System;
using System.Windows;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace MediaPlayer
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void mediaOpened(object sender, RoutedEventArgs e)
        {
            storyboard1.Begin();
            var ts = mediaPlayer.NaturalDuration.TimeSpan;
            leftTime.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            mediaSlider.Maximum = ts.TotalSeconds;
            mediaSlider.Minimum = 0;
        }

        private void stopButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            storyboard1.Stop();
        }

        private async void playButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                mediaPlayer.Play();
            }
            catch (Exception)
            {
                await new MessageDialog("播放失败，请尝试打开其他文件！").ShowAsync();
            }
            storyboard1.Begin();
        }

        private void pauseButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
            storyboard1.Pause();
        }

        private void ChangeMediaSpeedRatio(object sender, RoutedEventArgs args)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(mediaSlider.Value);
        }

        private void fullButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (mediaPlayer.IsFullWindow == false)
            {
                mediaPlayer.IsFullWindow = true;
            }
            else
            {
                mediaPlayer.IsFullWindow = false;
            }
        }

        private async void onTappedChangeVideoMethodButton(object sender, TappedRoutedEventArgs e)
        {
            Button currentTappedBtn = sender as Button;
            mediaPlayer.Pause();
            FileOpenPicker newmedia = new FileOpenPicker();
            newmedia.FileTypeFilter.Add(".mp3");
            newmedia.FileTypeFilter.Add(".mp4");
            newmedia.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            StorageFile file = await newmedia.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    var stream = await file.OpenAsync(FileAccessMode.Read);
                    mediaPlayer.SetSource(stream, file.ContentType);
                }
                catch (Exception)
                {
                    await new MessageDialog("打开文件失败，请重试！").ShowAsync();
                }
            }
        }
    }
}
