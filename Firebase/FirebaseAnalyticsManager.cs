using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseAnalyticsManager : MonoBehaviour
{
    public void Log(string event_name)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(event_name);
    }
    public void Log(string event_name, float val)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(event_name, event_name + "_param", val);
    }
    public void Log(string event_name, int val)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(event_name, event_name + "_param", val);
    }
}
