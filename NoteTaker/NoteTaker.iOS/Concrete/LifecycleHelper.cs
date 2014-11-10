using System;
using System.Collections.Generic;
using System.Text;
using MonoTouch.UIKit;
using NoteTaker.Interface;
using Xamarin.Forms;

[assembly : Dependency(typeof(NoteTaker.iOS.Concrete.LifecycleHelper))]

namespace NoteTaker.iOS.Concrete
{
    public class LifecycleHelper : ILifecycleHelper
    {
        public event Action Suspending;
        public event Action Resuming;

        public LifecycleHelper()
        {
            UIApplication.Notifications.ObserveDidEnterBackground((sender, args) =>
            {
                if (Suspending != null)
                    Suspending();
            });

            UIApplication.Notifications.ObserveWillEnterForeground((sender, args) =>
            {
                if (Resuming != null)
                    Resuming();
            });
        }
    }
}
