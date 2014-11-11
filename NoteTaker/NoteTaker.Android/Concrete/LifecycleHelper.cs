using NoteTaker.Interface;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker.Droid.Concrete.LifecycleHelper))]
namespace NoteTaker.Droid.Concrete
{
    public class LifecycleHelper : ILifecycleHelper
    {
        public event Action Suspending;
        public event Action Resuming;

        private static LifecycleHelper instance;

        public LifecycleHelper()
        {
            instance = this;
        }

        public static void OnPause()
        {
            if (instance != null && instance.Suspending != null)
            {
                instance.Suspending();
            }
        }

        public static void OnResume()
        {
            if (instance != null && instance.Resuming != null)
                instance.Resuming();
        }
    }
}