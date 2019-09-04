using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class TestAuth : MonoBehaviour
{
	void Awake ()
	{
		//fb初始化
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
			

		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}
	
	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}
	
	private void InitCallback ()
	{
		Debug.Log("success to Initialize the Facebook SDK");

		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...

		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
	
	
	
	// Use this for initialization
	void Start ()
	{
		
	}



	

	public void FaceBookAuth()
	{
		//FB登录拿token
		var perms = new List<string>(){"public_profile", "email"};
		FB.LogInWithReadPermissions(perms, delegate(ILoginResult result)
		{
			if (FB.IsLoggedIn) {
				// AccessToken class will have session details
				var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
					
				//拿到fb token 认证
				//FireBase Auth
				AuthWithFacebook(aToken.TokenString);
				
				// Print current access token's User ID
				Debug.Log(aToken.UserId);
				// Print current access token's granted permissions
				foreach (string perm in aToken.Permissions) {
					Debug.Log(perm);
				}
			} else {
				Debug.Log("User cancelled login");
			}
		});
	}

	public void GoogleAuth()
	{
		
	}

	private void AuthWithGoogle(string googleIdToken,string googleAccessToken)
	{
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		
		Firebase.Auth.Credential credential =
			Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
		auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("SignInWithCredentialAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
				return;
			}

			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
		});
	}

	private void AuthWithFacebook (string accessToken)
	{
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		
		Firebase.Auth.Credential credential =
			Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);
		auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("SignInWithCredentialAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
				return;
			}

			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
		});
	}
}
