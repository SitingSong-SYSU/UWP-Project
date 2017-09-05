using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPage), "");
        }
        private void visible_click(object sender, RoutedEventArgs e)
        {
            if (this.checkbox1.IsChecked == true)
            {
                this.line1.Visibility = Visibility.Visible;
            }
            else
            {
                this.line1.Visibility = Visibility.Collapsed;
            }
            if (this.checkbox2.IsChecked == true)
            {
                this.line2.Visibility = Visibility.Visible;
            }
            else
            {
                this.line2.Visibility = Visibility.Collapsed;
            }
        }
    }
}
