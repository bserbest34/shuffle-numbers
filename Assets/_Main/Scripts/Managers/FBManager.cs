using AudienceNetwork;
using Facebook.Unity;
using UnityEngine;
public class FBManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FB.Mobile.SetAdvertiserTrackingEnabled(true);
            AudienceNetworkAds.Initialize();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() =>
            {
                FB.ActivateApp();
                AudienceNetworkAds.Initialize();
            });
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
    }
}