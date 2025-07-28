using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Scene
{
    mm,gameplay
}
public class AudioManager : MonoBehaviour
{
    public Scene in_scene;
    [HideInInspector]public AudioSource[] audioSources;
    [HideInInspector]public AudioSource secondaryAudioSource,itemAudioSource,mech_Audio_source,powerUpAudioSource,timedItemAudioSource;

    public float volume;
    public AudioClip bg_freind,bg_enemy, mm_normal_button_click,mm_select_click,mm_buy_sound,wrong_clip,gun_shot,missile_launch,turret_entry,coin_selection
    ,item_select,ammo_pack,nectar_of_gods,help_from_above,thud_cover_no,green_no,negative_clash,death_blast,cover_no_blast,win_game,
    each_objective,rank_up,forfiet,beep,mine_blast,turret_shoot,rain_start,item_intro,reload,gameChanger,shieldOn,laserWhirring,portalOn,teleportAudio,pushItemAudio,emptyMag;
    
    public static AudioManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSources=GetComponents<AudioSource>();
        if(in_scene==Scene.mm)
        {
            secondaryAudioSource=audioSources[1];

        }
        else
        {
            secondaryAudioSource=audioSources[1];

            itemAudioSource=audioSources[2];
            mech_Audio_source=audioSources[3];
            powerUpAudioSource=audioSources[4];
            timedItemAudioSource=audioSources[5];
        }
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
    void Start()
    {
        for(int i=0;i<audioSources.Length;i++)
        {
            audioSources[i].volume=GameManager.instance.volume;
            if(in_scene==Scene.mm)
            {
                if(audioSources[0].volume>0.2f)
                {
                    audioSources[0].volume-=0.2f;
                }

            }
        }
        volume=GameManager.instance.volume;
        
    }
    public void AdjustGameVolume()
    {
        if(in_scene==Scene.mm)
        {
            for(int i=0;i<audioSources.Length;i++)
            {
                audioSources[i].volume=GameManager.instance.volume;
                if(audioSources[0].volume>0.2f)
                {
                    audioSources[0].volume-=0.2f;
                }

            }

        }
    }
    public void ButtonClickSound()
    {
        secondaryAudioSource.PlayOneShot(mm_normal_button_click);
    }
    public void SelectOptionSound()
    {
        secondaryAudioSource.PlayOneShot(mm_select_click);
    }
    public void BuyOptionSound()
    {
        secondaryAudioSource.PlayOneShot(mm_buy_sound);
    }
    public void ErrorSound()
    {
        secondaryAudioSource.PlayOneShot(wrong_clip);
    }
    public void GunShoot()
    {
        secondaryAudioSource.PlayOneShot(gun_shot);
    }
    public void EmptyMagSound()
    {
        secondaryAudioSource.PlayOneShot(emptyMag);
    }
    public void MissileLaunch()
    {
        secondaryAudioSource.PlayOneShot(missile_launch);
    }
    public void MissileBlast()
    {
        secondaryAudioSource.PlayOneShot(mine_blast);
    }

    public void InFriendlyGamePlay()
    {
        audioSources[0].clip=bg_freind;
        audioSources[0].Play();
    }

    public void InEnemyGamePlay()
    {
        audioSources[0].clip=bg_enemy;
        audioSources[0].Play();
    }
    public void PortalAudio()
    {
        
        timedItemAudioSource.PlayOneShot(portalOn);
    }
    public void pushAudioItem()
    {
        timedItemAudioSource.PlayOneShot(pushItemAudio);
    }

}
