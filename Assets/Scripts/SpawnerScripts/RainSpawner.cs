using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{

    public GameObject rain_number_prefab;

    [HideInInspector]public Transform rainMLeft,rainMRight,rainY;

    private bool spawnRain;

    [HideInInspector]public List<ParticleSystem> rainParticle=new List<ParticleSystem>();

    [HideInInspector]public Transform[] spawnRainParticleSystem;

    public ParticleSystem RainInitiation_prefab;

    public float current_time=0,fireRate=0.2f; 

    [HideInInspector]public bool callFrom;
    
    public void StartRainSequence(bool callBy)
    {
        StartCoroutine(StartRain());
        for(int i=0;i<spawnRainParticleSystem.Length;i++)
        {
            var rain=Instantiate(RainInitiation_prefab,spawnRainParticleSystem[i].position,Quaternion.identity);
            rain.gameObject.SetActive(false);
            var main=rain.main;
            if(callBy)
            {
                main.startColor=Color.green;
            }
            else
            {
                main.startColor=Color.red;
            }
            rain.gameObject.SetActive(true);
            rainParticle.Add(rain);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnRain==true)
        {
            if(current_time>(fireRate))
            {
                current_time=0;
                SpawnRain(callFrom);
            }
            current_time+=Time.deltaTime;
        }
    }
    void SpawnRain(bool callBy)
    {
        var x_pos=Random.Range(rainMLeft.position.x,rainMRight.position.x);
        var temp_rain=Instantiate(rain_number_prefab,new Vector3(x_pos,rainY.position.y,0),Quaternion.identity);
        temp_rain.SetActive(false);
        temp_rain.GetComponent<RainCharacter>().callBy=callBy;
        temp_rain.GetComponent<RainCharacter>().SettingCharactersForNumber();
        temp_rain.SetActive(true);
        temp_rain.GetComponent<RainCharacter>().StartDestructionCroutine();
        
    }
    IEnumerator StopRain()
    {
        yield return new WaitForSeconds(8f);
        spawnRain=false;    
        Destroy(gameObject);    
    }
    IEnumerator StartRain()
    {
        yield return new WaitForSeconds(4f);
        spawnRain=true;
        if(callFrom)
        {
            AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.help_from_above,AudioManager.instance.volume);
        }
        else
        {
            AudioManager.instance.mech_Audio_source.PlayOneShot(AudioManager.instance.help_from_above,AudioManager.instance.volume);
        }

        for(int i=0;i<rainParticle.Count;i++)
        {
            Destroy(rainParticle[i]);
            
        }
        rainParticle.Clear();
        StartCoroutine(StopRain());
    }
}
