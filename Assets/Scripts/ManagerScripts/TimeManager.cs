using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float durationOfSlowmo,allowSloMoDuration;

    
    [HideInInspector]public bool notAllowSlomo;

    public float lagOfSlowMo;

    public bool InSlowMo;
    public static TimeManager instance;
    
    private void Awake()
    {
        MakeInstance();
    }
    void MakeInstance()
    {
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void startSlowmoCoroutine()
    {
        
        //if the camera not shaking only then initialize the slow mo         
        if(!Camera.main.GetComponent<CameraScript>().inCameraShake)
        {
            notAllowSlomo=true;

            StartCoroutine(startslowmolag());
            
        }
    }
    IEnumerator startslowmolag()
    {
        yield return new WaitForSeconds(lagOfSlowMo);

        InSlowMo=true;
        for(var i=0;i<AudioManager.instance.audioSources.Length;i++)
        {
            AudioManager.instance.audioSources[i].pitch=0.4f;
        }
        Time.timeScale=0.01f;
        //disabling the controls for the player
        CrossHairMovement.instance.cantBeControlled=true;
        circleMovement.instance.cantBeControlled=true;
        CannonCharacter.instance.cantBeControlled=true;
        
    
        StartCoroutine(cancelSlowMo());
    }
    IEnumerator cancelSlowMo()
    {
        yield return new WaitForSecondsRealtime(durationOfSlowmo);
        for(var i=0;i<AudioManager.instance.audioSources.Length;i++)
        {
            AudioManager.instance.audioSources[i].pitch=1f;
        }

        Time.timeScale=1f;
        InSlowMo=false;
        //disabling the controls for the player
        CrossHairMovement.instance.cantBeControlled=false;
        circleMovement.instance.cantBeControlled=false;
        CannonCharacter.instance.cantBeControlled=false;
        StartCoroutine(allowSloMo());
    }
    IEnumerator allowSloMo()
    {
        yield return new WaitForSeconds(allowSloMoDuration);
        notAllowSlomo=false;

    }
    public void StartSloInstant()
    {
        //if the camera not shaking only then initialize the slow mo         
        if(!Camera.main.GetComponent<CameraScript>().inCameraShake)
        {
            notAllowSlomo=true;
            for(var i=0;i<AudioManager.instance.audioSources.Length;i++)
            {
                AudioManager.instance.audioSources[i].pitch=0.4f;
            }

            StartCoroutine(cancelSlowMo());

            InSlowMo=true;
            CrossHairMovement.instance.cantBeControlled=true;
            circleMovement.instance.cantBeControlled=true;
            CannonCharacter.instance.cantBeControlled=true;

            Time.timeScale=0.01f;
        }
    }

}
