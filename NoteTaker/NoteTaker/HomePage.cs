using System;
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


            CheckSavedInfo();
        }

        private async void CheckSavedInfo()
        {
            if (await FileHelper.ExistsAsync(App.TransientFileName))
            {
                string str = await FileHelper.ReadTextAsync(App.TransientFileName);

                await FileHelper.DeleteFileAsync(App.TransientFileName);

                string[] contents = str.Split('\x1F');
                string filename = contents[0];
                bool isNoteEdit = Boolean.Parse(contents[1]);
                string entryText = contents[2];
                string editorText = contents[3];

                var note = new Note(filename);
                note.Title = entryText;
                note.Text = editorText;

                var notePage = new NotePage(note, isNoteEdit);
                await Navigation.PushAsync(notePage);

            }
        }
    }
}
