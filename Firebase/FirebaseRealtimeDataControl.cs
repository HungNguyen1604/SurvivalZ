using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using VoxelBusters.CoreLibrary;
using Newtonsoft.Json;
using Firebase.Extensions;
using System.Reflection.Emit;
using System;


public class FirebaseRealtimeDataControl : BySingleton<FirebaseRealtimeDataControl>
{
    private DatabaseReference userdata_p;
    [SerializeField] private DataModel dataModel;
    private UserData userData;
    int ver = 0;

    public bool isGetDataDone = false;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void CheckDataServer()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(" id :" + FirebaseAuthenicationManager.instance.firebase_Account);
        userdata_p = reference.Child("userdata").Child(FirebaseAuthenicationManager.instance.firebase_Account);
        userdata_p.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(" data firebase isfaulted");
                userData = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value != null)
                {
                    Debug.Log(" data firebase" + snapshot.GetRawJsonValue());
                    userData = JsonConvert.DeserializeObject<UserData>(snapshot.GetRawJsonValue());
                }
                else
                {
                    Debug.Log(" data empty");
                    userData = null;
                }
            }
            isGetDataDone = true;
        });
    }

    public void Read(string path, Action<string> callback)
    {
        userdata_p.Child(path).GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                Debug.Log(" data firebase isfaulted");
                callback(string.Empty);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value != null)
                {
                    Debug.Log(" data firebase" + snapshot.GetRawJsonValue());
                    callback(snapshot.GetRawJsonValue());
                }
                else
                {
                    Debug.Log(" data empty");
                    callback(string.Empty);
                }
            }
        });
    }

    private void Version_Signin_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            int ver_new = int.Parse(e.Snapshot.Value.ToString());
            if (ver_new > ver)
            {
                DialogManager.instance.ShowDialog(DialogIndex.DialogTheSameAccount);
            }
            Debug.Log("Version_Signin_ValueChanged : " + ver_new);
        }
    }

    public UserData GetData()
    {
        return userData;
    }

    public void Update_Version_Signin()
    {
        userdata_p.Child("version_sign_in").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(" version_sign_in isfault!");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value != null)
                {
                    ver = int.Parse(snapshot.Value.ToString());
                    ver++;
                    userdata_p.Child("version_sign_in").SetValueAsync(ver).ContinueWith(task =>
                    {
                        userdata_p.Child("version_sign_in").ValueChanged += Version_Signin_ValueChanged;
                    });
                }
                else
                {
                    Debug.Log(" version_sign_in empty");
                }
            }
            isGetDataDone = true;
        });
    }

    public void SaveAllData()
    {
        string json = JsonConvert.SerializeObject(dataModel.GetAllData());

        userdata_p.SetRawJsonValueAsync(json);
    }
}
