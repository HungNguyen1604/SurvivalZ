using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLConfig : MonoBehaviour
{
    public static string ROOT_CLOUD_URL = "https://us-central1-survival-z-1eec1.cloudfunctions.net/api";
    //public static string ROOT_CLOUD_URL = "http://localhost:5000/survival-z-1eec1/us-central1/api";



    public static string POST_GETSERVERTIME = "utilities/getServerTime";
    public static string POST_CREATE_DATA = "data/createNewUser";
    public static string POST_UPDATE_DATA = "data/updateAllData";
    public static string POST_GET_DATA = "data/getUserData";
}
