using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Interface;
using Xamarin.Forms;

namespace NoteTaker
{
    public class Note: INotifyPropertyChanged
    {
        public Note(string filename)
        {
            this.Filename = filename;
        }
        public string title, text, identifier;

        public string Title
        {
            set { SetProperty(ref title, value); }
            get { return title; }
        }

        public string Identifier
        {
            private set { SetProperty(ref identifier, value); }
            get { return identifier; }
        }

        string MakeIdentifier()
        {
            if (!string.IsNullOrWhiteSpace(this.Title))
                return title;
            const int truncationLength = 30;
            if (this.Text == null ||
            this.Text.Length <= truncationLength)
            {
                return this.Text;
            }
            string truncated = this.Text.Substring(0, truncationLength);
            int index = truncated.LastIndexOf(' ');
            if (index != -1)
                truncated = truncated.Substring(0, index);
            return truncated;
        }
        public string Text
        {
            set { SetProperty(ref text, value); }
            get { return text; }
        }

        public async Task SaveAsync()
        {
            string texts = Title + "\n" + Text;
            await FileHelper.WriteTextAsync(Filename, texts);
        }

        public async Task LoadAsync()
        {

            string texts = await FileHelper.ReadTextAsync(Filename);
            
            int index = texts.IndexOf('\n');
            Title = texts.Substring(0, index);
            Text = texts.Substring(index + 1);           
        }

        public async Task DeleteAsync()
        {
            await FileHelper.DeleteFileAsync(Filename);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Filename { private set; get; }
    

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                return Title;
            }

            const int truncationLength = 30;

            if (this.Text.Length <= truncationLength)
                return Text;

            var truncated = Text.Substring(0, truncationLength);
            int index = truncated.LastIndexOf(' ');

            if (index != -1)
                truncated = truncated.Substring(0, index);

            return truncated;
        }
    }
}
