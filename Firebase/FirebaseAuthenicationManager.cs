using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class FirebaseAuthenicationManager : BySingleton<FirebaseAuthenicationManager>
{
    FirebaseAuth auth;
    FirebaseUser user = null;
    private string fb_uuid;

    public string firebase_Account
    {
        get
        {
            return "test acc";
        }
        set
        {
            fb_uuid = value;
        }
    }
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);


    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                firebase_Account = user.UserId;
                Debug.Log("Signed in " + user.UserId);
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }
    public void CheckAuthenication()
    {
        if (user != null)
        {
            Debug.LogError("CheckAuthenication user !=null.");
            firebase_Account = user.UserId;
        }
        else
        {
            StartCoroutine("StartCheckAccount");
        }
    }

    IEnumerator StartCheckAccount()
    {
        yield return new WaitForEndOfFrame();

        Debug.LogError("CheckAuthenication SignInWithEmailAndPasswordAsync.");
        bool is_SignInDone = false;
        string email = GameServiceManager.instance.UUID() + "@wf.com";
        string password = "12345678";
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            }
            else
            {
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);

                firebase_Account = result.User.UserId;
            }
            is_SignInDone = true;
        });
        yield return new WaitUntil(() => is_SignInDone);
        if (firebase_Account == string.Empty)
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
                firebase_Account = result.User.UserId;

            });
        }
    }
    public void CheckHasUser()
    {

    }


    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
