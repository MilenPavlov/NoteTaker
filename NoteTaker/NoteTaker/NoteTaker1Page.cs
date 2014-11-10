using NoteTaker.Interface;
using Xamarin.Forms;

namespace NoteTaker
{
    public class NoteTaker1Page : ContentPage
    {
        private static readonly string FILENAME = "test.note";    

        public NoteTaker1Page()
        {
            var entry = new Entry
            {
                Placeholder = "Title (optional)"
            };

            var editor = new Editor
            {
                Keyboard = Keyboard.Create(KeyboardFlags.All),
                BackgroundColor = Device.OnPlatform(Color.Default, Color.Default, Color.White),
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var saveButton = new Button
            {
                Text = "Save",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            

            var loadButton = new Button
            {
                Text = "Load",           
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsEnabled = false

            };

            var note = new Note("");
            BindingContext = note;
            entry.SetBinding(Entry.TextProperty, "Title");
            editor.SetBinding(Editor.TextProperty, "Text");

            saveButton.Clicked += async (sender, args) =>
            {
                saveButton.IsEnabled = false;
                loadButton.IsEnabled = false;
                await note.SaveAsync();
                saveButton.IsEnabled = true;
                loadButton.IsEnabled = true;
            };

            loadButton.Clicked += async (sender, args) =>
            {
                saveButton.IsEnabled = false;
                loadButton.IsEnabled = false;
                await note.LoadAsync();
                saveButton.IsEnabled = true;
                loadButton.IsEnabled = true;
            };

            FileHelper.ExistsAsync(FILENAME).ContinueWith((task) => loadButton.IsEnabled = task.Result);

       

            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 0);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Title:"
                    },
                    entry,
                    new Label
                    {
                        Text = "Note:"
                    },
                    editor,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            saveButton,
                            loadButton
                        }
                    }
                }
            };
        }
    }
}
