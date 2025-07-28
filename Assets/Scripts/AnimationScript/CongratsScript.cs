using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratsScript : MonoBehaviour
{
    public AudioSource _this_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _this_AudioSource=GetComponent<AudioSource>();
        _this_AudioSource.volume=GameManager.instance.volume;
    }
   public void congratsIntro()
   {
       _this_AudioSource.volume=GameManager.instance.volume;
       AudioManager.instance.audioSources[0].Stop();
       _this_AudioSource.clip=AudioManager.instance.rank_up;
       _this_AudioSource.Play();
   }
   public void ObjectiveEffect()
   {
       _this_AudioSource.Stop();
       _this_AudioSource.clip=AudioManager.instance.each_objective;
       _this_AudioSource.Play();

   }
   public void StartSound()
   {
       AudioManager.instance.audioSources[0].Play();

   }
}
