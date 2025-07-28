using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public IEnumerator camerashakeCoroutine;

    public bool inCameraShake;
    //camera shake
    public IEnumerator cameraShake(float time,float intensity)
    {
        inCameraShake=true;
        float xaxis, yaxis;
        Vector3 originalPosition = transform.position;
        float timer = 0f;
        while(timer<time)
        {
            if(Time.timeScale>=1f)
            {
                Vector3 cameraShake = transform.position;
                xaxis = cameraShake.x += Random.Range(-1f, 1f) * intensity;
                yaxis = cameraShake.y += Random.Range(-1f, 1f) * intensity;
                transform.position = cameraShake;
                timer += Time.deltaTime;
                //skips for one frame
                yield return 0;

            }
            else
            {
                break;
            }
        }
        transform.position = originalPosition;
        inCameraShake=false;
    }
    //for calls fromn the already dead bullets
    public void startCoroutine(float time,float intensity)
    {
        if(!TimeManager.instance.InSlowMo)
        {
            StartCoroutine(cameraShake(time,intensity));
        }
    }

  
}
