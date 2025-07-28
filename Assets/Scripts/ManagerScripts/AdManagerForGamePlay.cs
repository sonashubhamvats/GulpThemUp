using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
public class AdManagerForGamePlay : MonoBehaviour,IUnityAdsListener
{
    string gameId = "3920035";

    string myPlacementId = "rewardedVideo";

    public TextMeshProUGUI watch_ad_text;

    public Button watch_ad_button;

    public GameObject watchAd,player;
    public ItemSlotInGamePlay itemSlotInGamePlay;
    //for testing purposes in the finalbuild make it false 
    bool testMode = true;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);

    }
    public void ShowRewardedVideo() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId)) {
            watchAd.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TimerScriptWatchAd>().Stop();
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
            watch_ad_button.gameObject.SetActive(false);
            watch_ad_text.text="You dont get a chance twice!!";
            watchAd.SetActive(false);
            Time.timeScale=1f;
            itemSlotInGamePlay.UseNectarOfTheGods(true);
            GameConclusionScript.instance.deathSequence=false;
            // Reward the user for watching the ad to completion.
        } else if (showResult == ShowResult.Skipped) {
            AudioManager.instance.audioSources[0].Play();

            watch_ad_button.gameObject.SetActive(false);
            watch_ad_text.text="You dont get a chance twice!!";

            // Do not reward the user for skipping the ad.
        } else if (showResult == ShowResult.Failed) {
            AudioManager.instance.audioSources[0].Play();

            watch_ad_button.gameObject.SetActive(false);
            watch_ad_text.text="You dont get a chance twice!!";

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
        AudioManager.instance.audioSources[0].Pause();

    } 

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }

}
