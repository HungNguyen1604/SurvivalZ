using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : BySingleton<AnalyticsManager>
{
    public FirebaseAnalyticsManager firebaseAnalyticsManager_;

    public void Log(string event_name)
    {
        firebaseAnalyticsManager_.Log(event_name);
    }
    public void Log(string event_name, float val)
    {
        firebaseAnalyticsManager_.Log(event_name, val);
    }
    public void Log(string event_name, int val)
    {
        firebaseAnalyticsManager_.Log(event_name, val);
    }
}
