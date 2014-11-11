using System;

namespace NoteTaker.Interface
{
    public interface ILifecycleHelper
    {
        event Action Suspending;
        event Action Resuming;
    }
}
