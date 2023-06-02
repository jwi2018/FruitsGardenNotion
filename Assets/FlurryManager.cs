using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FlurrySDK;

public class FlurryManager : Singleton<FlurryManager>
{
#if UNITY_ANDROID
    private string FLURRY_API_KEY = "PXYMP8RXYXYM6PCDXNV6";
#elif UNITY_IPHONE
    //private string FLURRY_API_KEY = "5DKS36JRFR8M4QYV43MF";
#else
    private string FLURRY_API_KEY = null;
#endif

    public void Init()
    {
        // Initialize Flurry.
        new Flurry.Builder()
                  .WithCrashReporting(true)
                  .WithLogEnabled(true)
                  .WithLogLevel(Flurry.LogLevel.VERBOSE)
                  .WithMessaging(true)
                  .Build(FLURRY_API_KEY);
    }
}
