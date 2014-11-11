using NoteTaker.Interface;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaker
{
    public class NotePage : ContentPage
    {
        private Note _note;
        private bool _isNoteEdit;
        private ILifecycleHelper helper = DependencyService.Get<ILifecycleHelper>();

        public NotePage(Note note, bool isNoteEdit = false)
        {
            _note = note;
            _isNoteEdit = isNoteEdit;
            Title = _isNoteEdit ? "Edit Note" : "New Note";

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

            BindingContext = _note;
            entry.SetBinding(Entry.TextProperty, "Title");
            editor.SetBinding(Editor.TextProperty, "Text");

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label {Text = "Title: "},
                    entry,
                    new Label {Text = "Note: "},
                    editor
                }
            };

            if (isNoteEdit)
            {
                var cancelItem = new ToolbarItem
                {
                    Name = "Cancel",
                    Icon = Device.OnPlatform("cancel.png",
                        "ic_action_cancel.png",
                        "Images/cancel.png"),
                    Order = ToolbarItemOrder.Primary
                };

                cancelItem.Activated += async (sender, args) =>
                {
                    bool confirm = await DisplayAlert("Note Taker", "Cancel note edit?", "Yes", "No");

                    if (confirm)
                    {
                        await note.LoadAsync();
                        await Navigation.PopAsync();
                    }
                };

                ToolbarItems.Add(cancelItem);
                var deleteItem = new ToolbarItem
                {
                    Name = "Delete",
                    Icon = Device.OnPlatform("discard.png",
                        "ic_action_discard.png",
                        "Images/delete.png"),
                    Order = ToolbarItemOrder.Primary
                };

                deleteItem.Activated += async (sender, args) =>
                {
                    bool confirm = await DisplayAlert("Note Taker", "Delete this note?", "Yes", "No");
                    if (confirm)
                    {
                        await note.DeleteAsync();
                        App.NoteFolder.Notes.Remove(_note);

                        await Navigation.PopAsync();
                    }
                };

                ToolbarItems.Add(deleteItem);
            }
            Content = stackLayout;
        }

        


        protected override void OnAppearing()
        {
            helper.Suspending += OnSuspending;
            helper.Resuming += OnResuming;
            base.OnAppearing();
        }

        async void OnResuming()
        {
            if (await FileHelper.ExistsAsync(App.TransientFileName))
            {
                await FileHelper.DeleteFileAsync(App.TransientFileName);
            }
        }

        void OnSuspending()
        {
            var str = _note.Filename + "\x1F" + _isNoteEdit.ToString() + "\x1F" + _note.Title + "\x1F" + _note.Text;
            Task task = Task.Run(() => FileHelper.WriteTextAsync(App.TransientFileName, str));
            task.Wait();
        }


        protected async override void OnDisappearing()
        {
            if (!string.IsNullOrWhiteSpace(_note.Text) || !string.IsNullOrWhiteSpace(_note.Title))
            {
                await _note.SaveAsync();
                if (!_isNoteEdit)
                {
                    App.NoteFolder.Notes.Add(_note);
                }
            }
            base.OnDisappearing();
        }
    }
}
