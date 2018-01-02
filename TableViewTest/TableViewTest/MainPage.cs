using System;
using Xamarin.Forms;

namespace TableViewTest
{
    class MainPage : ContentPage
    {
        public MainPage()
        {
            var b1 = new Button
            {
                Text = "Custom Table View",
                Command = new Command(async () => await Navigation.PushAsync(new TablePage()))
            };

            var stackLayout = new StackLayout
            {
                Children = { b1 }
            };

            Title = "Main";
            Content = stackLayout;
        }
    }
}
