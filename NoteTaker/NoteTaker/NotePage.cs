using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaker
{
    public class NotePage : ContentPage
    {
        private string _filename;
        Note _note;
        private bool _isNoteEdit;

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
                    new Label { Text = "Title: "},
                    entry,
                    new Label { Text = "Note: "},
                    editor
                }
            };

            if (isNoteEdit)
            {
                var cancelButton = new Button
                {
                    Text = "Cancel",
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                cancelButton.Clicked += async (sender, args) =>
                {
                    bool confirm = await DisplayAlert("Note Taker", "Cancel note edit?", "Yes", "No");

                    if (confirm)
                    {
                        await note.LoadAsync();
                        await Navigation.PopAsync();
                    }
                };

                var deleteButton = new Button
                {
                    Text = "Delete",
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                deleteButton.Clicked += async (sender, args) =>
                {
                    bool confirm = await DisplayAlert("Note Taker", "Delete this note?", "Yes", "No");
                    if (confirm)
                    {
                        await note.DeleteAsync();
                        App.NoteFolder.Notes.Remove(_note);

                        await Navigation.PopAsync();
                    }
                };

                var horzStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        cancelButton, deleteButton
                    }
                };

                stackLayout.Children.Add(horzStack);
            }

            Content = stackLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
