using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaker
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Title = "Note Taker X";

            var listView = new ListView
            {
                ItemsSource = App.NoteFolder.Notes,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var button = new Button
            {
                Text = "Add new Note",
                HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center
                            
            };

            button.Clicked += (sender, args) =>
            {
                var dateTime = DateTime.UtcNow;
                string fileName = dateTime.ToString("yyyyMMddHHmmssfff") + ".note";
                var note = new Note(fileName);
                Navigation.PushAsync(new NotePage(note));
            };

            listView.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem != null)
                {
                    listView.SelectedItem = null;

                    var note = (Note) args.SelectedItem;

                    Navigation.PushAsync(new NotePage(note, true));
                }
            };

            Content = new StackLayout
            {
                Children =
                {
                    listView,
                    button
                }
            };
        }   
    }
}
