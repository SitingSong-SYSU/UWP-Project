using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Todos.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _title;
        private ImageSource _img;
        public string details { get; set; }
        public DateTime date { get; set; }
        public bool? finish { get; set; }

        public TodoItem(DateTime date, ImageSource img, string title = "", string details = "", bool finish = false)
        {
            this.date = date;
            this.img = (img == null ? new BitmapImage(new Uri("ms-appx:///Assets/fruit.jpg")) : img);
            this.title = title;
            this.details = details;
            this.finish = finish;
        }

        private void NotifyPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }

        public string title
        {
            set
            {
                _title = value;
                NotifyPropertyChanged("title");
            }
            get
            {
                return _title;
            }
        }

        public ImageSource img
        {
            set
            {
                _img = value;
                NotifyPropertyChanged("img");
            }
            get
            {
                return _img;
            }
        }

        public void UpdateItem(DateTime date, ImageSource img, string title, string details)
        {
            this.date = date;
            this.img = img;
            this.title = title;
            this.details = details;
        }
    }
}