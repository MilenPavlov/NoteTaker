
using Xamarin.Forms;

namespace NoteTaker
{
    public class App
    {
        static NoteFolder noteFolder = new NoteFolder();

        internal static readonly string TransientFileName = "TransientData.save";

        internal static NoteFolder NoteFolder
        {
            get { return noteFolder; }
        }
        public static Page GetMainPage()
        {
            return new NavigationPage(new HomePage());
        }
    }
}
