using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Advertisements;
public class Admanager : MonoBehaviour,IUnityAdsListener
{
    string gameId = "3920035";

    public TextMeshProUGUI coinCount,recievedMoneyPanelText;

    public GameObject recievedMoneyPanel;
    string myPlacementId = "rewardedVideo";
    
    //for testing purposes in the finalbuild make it false 
    bool testMode = true;

    void Start () 
    {
        Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        if(GameManager.instance.levelCounter_ad==3)
        {
            GameManager.instance.levelCounter_ad=0;
            
            ShowInterstitialAd();
        }
    }

    public void ShowInterstitialAd() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) {
            Advertisement.Show();
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    public void ShowRewardedVideo() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId)) {
            Advertisement.Show(myPlacementId);
        } 
        else {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            AudioManager.instance.audioSources[0].Play();

            if(placementId==myPlacementId)
            {
                AudioManager.instance.BuyOptionSound();
                recievedMoneyPanel.SetActive(true);
                recievedMoneyPanelText.text="Congrats!!<br>You recieved 10 coins";
                int curr_count=(int)double.Parse(coinCount.text);
                curr_count+=10;
                coinCount.text=""+curr_count;
                GameManager.instance.CoinCount+=10;

            }
            // Reward the user for watching the ad to completion.
        } else if (showResult == ShowResult.Skipped) {
            AudioManager.instance.audioSources[0].Play();

            // Do not reward the user for skipping the ad.
        } else if (showResult == ShowResult.Failed) {
            AudioManager.instance.audioSources[0].Play();

            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId) {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
        AudioManager.instance.audioSources[0].Stop();

    } 

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }
}

