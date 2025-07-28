using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessingScript : MonoBehaviour
{
    private Volume volume;
    [Range(0f,0.75f)]
    private float bloomValue;
    
    [Range(0f,0.8f)]
    private float vignetteValue;

    public static PostProcessingScript instance;

    private void Awake() {
        volume=GetComponent<Volume>();
        
        MakeInstance();
    }
    // Start is called before the first frame update
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DecreaseGloomAndIncreaseVignette()
    {
        Bloom bloom;
        Vignette vignette;
        Debug.Log("Decrease the vignette");
        if(volume.profile.TryGet<Bloom>(out bloom))
        {
            bloomValue=bloom.intensity.value;
            bloomValue-=0.2f;
            bloomValue=Mathf.Clamp(bloomValue,0f,0.75f);
            bloom.intensity.value=bloomValue;
        }
        if(volume.profile.TryGet<Vignette>(out vignette))
        {
            vignetteValue=vignette.intensity.value;
            vignetteValue+=0.2f;
            vignetteValue=Mathf.Clamp(vignetteValue,0f,0.6f);
            vignette.intensity.value=vignetteValue;
        }

    }

    public void ToggleChromaticAbrassion(bool check)
    {
        ChromaticAberration chromatic;
        if(volume.profile.TryGet<ChromaticAberration>(out chromatic))
        {
            if(!check)
            {
                chromatic.intensity.value=0.5f;

            }
            else
            {
                chromatic.intensity.value=0f;
            }
        }
    }

    public void UltraGlowGloom(bool check,bool increase=true)
    {
        Bloom bloom;
        if(volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.threshold.value=0.7f;
            bloom.intensity.value=3f;
        }
        if(check)
        {
            StartCoroutine(reverseTheGloomEffectInstant());
        }
        else
        {
            StartCoroutine(reverseTheGloomEffectInstantLag(increase));        
        }
    }
    IEnumerator reverseTheGloomEffectInstant()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        Bloom bloom;
        if(volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.threshold.value=1f;
            bloom.intensity.value=bloomValue;
            Debug.Log("Before-"+bloomValue);
            Vignette vignette;
            bloomValue=bloom.intensity.value;
            Debug.Log("After-"+bloomValue);
            bloomValue+=0.1f;
            bloomValue=Mathf.Clamp(bloomValue,0f,0.75f);
            bloom.intensity.value=bloomValue;

            if(volume.profile.TryGet<Vignette>(out vignette))
            {
                vignetteValue=vignette.intensity.value;
                vignetteValue-=0.3f;
                vignetteValue=Mathf.Clamp(vignetteValue,0f,0.8f);
                vignette.intensity.value=vignetteValue;
            }

        }

    }

    IEnumerator reverseTheGloomEffectInstantLag(bool increase)
    {
        yield return new WaitForSecondsRealtime(1.7f);
        Bloom bloom;
        if(volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.threshold.value=1f;
            bloom.intensity.value=bloomValue;
            if(increase)
            {
                Vignette vignette;
                bloomValue=bloom.intensity.value;
                bloomValue+=0.1f;
                bloomValue=Mathf.Clamp(bloomValue,0f,0.75f);
                bloom.intensity.value=bloomValue;

                if(volume.profile.TryGet<Vignette>(out vignette))
                {
                    vignetteValue=vignette.intensity.value;
                    vignetteValue-=0.3f;
                    vignetteValue=Mathf.Clamp(vignetteValue,0f,0.8f);
                    vignette.intensity.value=vignetteValue;
                }

            }

        }

    }

}
