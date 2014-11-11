
using Android.App;
using Android.Content.PM;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace NoteTaker.Droid
{
	[Activity(Label = "NoteTaker", MainLauncher = true, Icon = "@drawable/ic_action_share", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            SetPage(App.GetMainPage());
        }

        protected override void OnPause()
        {
            Concrete.LifecycleHelper.OnPause();
            base.OnPause();
        }

	    protected override void OnResume()
	    {
            Concrete.LifecycleHelper.OnResume();
	        base.OnResume();
	    }
    }
}

