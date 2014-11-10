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
                ItemTemplate = new DataTemplate(typeof(TextCell)),
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Identifier");

            listView.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem != null)
                {
                    listView.SelectedItem = null;

                    var note = (Note) args.SelectedItem;

                    Navigation.PushAsync(new NotePage(note, true));
                }
            };

			var addNewItem = new ToolbarItem {
				Name = "Add Note",
				Icon = Device.OnPlatform ("new.png", 
					"ic_action_new.png", 
					"Images/add.png"),
				Order = ToolbarItemOrder.Primary
			};

			addNewItem.Activated += (object sender, EventArgs e) => 
			{
				var dateTime = DateTime.UtcNow;
				string fileName = dateTime.ToString("yyyyMMddHHmmssfff") + ".note";
				var note = new Note(fileName);
				Navigation.PushAsync(new NotePage(note));
			};

            ToolbarItems.Add(addNewItem);

            Content = listView;
        }   
    }
}
