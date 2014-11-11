using System.Collections.ObjectModel;
using System.Linq;

namespace NoteTaker
{
    public class NoteFolder
    {
        public NoteFolder()
        {
            Notes = new ObservableCollection<Note>();
            GetFilesAsync();
        }

        private async void GetFilesAsync()
        {
            var filenames =
                from filename in await FileHelper.GetFilesAsync()
                where filename.EndsWith(".note")
                orderby filename
                select filename;

            foreach (var filename in filenames)
            {
                var note = new Note(filename);
                await note.LoadAsync();
                Notes.Add(note);
            }
        }

        public ObservableCollection<Note> Notes { get; private set; }
    }
}
