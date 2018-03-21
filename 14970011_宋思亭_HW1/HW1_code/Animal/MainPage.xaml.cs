using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Animal
{
    public sealed partial class MainPage : Page
    {
        private delegate string AnimalSaying(object sender);
        private event AnimalSaying Say;

        public MainPage()
        {
            this.InitializeComponent();
        }

        interface Animal
        {
            string saying(object sender);
            int A { get; set; }
        }

        class cat : Animal
        {
            TextBlock word;
            private int a;

            public cat(TextBlock w)
            {
                this.word = w;
            }
            public string saying(object sender)
            {
                this.word.Text += "Cat: I am a cat.\n";
                return "";
            }
            public int A
            {
                get { return a; }
                set { this.a = value; }
            }
        }

        class dog : Animal
        {
            TextBlock word;
            private int a;

            public dog(TextBlock w)
            {
                this.word = w;
            }
            public string saying(object sender)
            {
                this.word.Text += "Dog: I am a dog.\n";
                return "";
            }
            public int A
            {
                get { return a; }
                set { this.a = value; }
            }
        }

        class pig : Animal
        {
            TextBlock word;
            private int a;

            public pig(TextBlock w)
            {
                this.word = w;
            }
            public string saying(object sender)
            {
                this.word.Text += "Pig: I am a pig.\n";
                return "";
            }
            public int A
            {
                get { return a; }
                set { this.a = value; }
            }
        }

        private cat c;
        private dog d;
        private pig p;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.words.Text = "";
            Random i = new Random();
            int x = i.Next(3);
            if (x == 0)
            {
                c = new cat(words);
                Say += new AnimalSaying(c.saying);
            }
            else if (x == 1)
            {
                d = new dog(words);
                Say += new AnimalSaying(d.saying);
            }
            else if (x == 2)
            {
                p = new pig(words);
                Say += new AnimalSaying(p.saying);
            }
            Say(this);
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.words.Text = "";
            if (this.textBox.Text == "cat" || this.textBox.Text == "Cat")
            {
                c = new cat(words);
                Say += new AnimalSaying(c.saying);
            }
            else if (this.textBox.Text == "dog" || this.textBox.Text == "Dog")
            {
                d = new dog(words);
                Say += new AnimalSaying(d.saying);
            }
            else if (this.textBox.Text == "pig" || this.textBox.Text == "Pig")
            {
                p = new pig(words);
                Say += new AnimalSaying(p.saying);
            }
            Say(this);
            this.textBox.Text = "";
        }
    }
}
