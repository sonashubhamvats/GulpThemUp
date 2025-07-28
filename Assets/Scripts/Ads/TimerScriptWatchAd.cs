using System.Collections;
using UnityEngine;
using TMPro;
public class TimerScriptWatchAd : MonoBehaviour
{
    public TextMeshProUGUI timer;


    private int time;
    // Start is called before the first frame update
    void Start()
    {
        time=(int)double.Parse(timer.text);
        StartCoroutine(decrease());
    }
   public IEnumerator decrease()
    {
        yield return new WaitForSecondsRealtime(1f);
        time--;
        AudioManager.instance.secondaryAudioSource.PlayOneShot(AudioManager.instance.beep);
        timer.text=""+time;
        if(time>=1)
        {
            StartCoroutine(decrease());
        
        }
        else
        {
            ButtonManager.instance.forfeitAccepted();
        }
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
}
