using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameConclusionScript : MonoBehaviour
{
    public TextMeshProUGUI playerPointText,level_complete_panel_xp_points;
    public GameObject levelComplete;
    public TextMeshProUGUI[] objectives,indi_expPoints;
    public Color gray;
    public Image[] star,checks;
    public GameObject player,watch_ad_panel;
    public static GameConclusionScript instance;
    [HideInInspector]public bool deathSequence;
    private int pointThreshHold;

    private void Awake()
    {
        MakeInstance();
        pointThreshHold=GameManager.instance.objNos[0];
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pointThreshHold>=(int)double.Parse(playerPointText.text)&&!deathSequence)
        {
            player.GetComponent<AudioSource>().PlayOneShot(AudioManager.instance.death_blast,AudioManager.instance.volume);
            circleMovement.instance.cantBeControlled=true;
            TouchMovement.instance.clutch=true;
            FindObjectOfType<PlayerAttack>().fire=false;
            AudioManager.instance.mech_Audio_source.Pause();
            AudioManager.instance.itemAudioSource.Pause();
            ParticleInitiation.instance.CoverParticleDestruction(player.transform.position,player.GetComponent<SpriteRenderer>().color);    
            deathSequence=true;
            InvokeRepeating("Vibrate",0f,1f);
            StartCoroutine(playerDestruction());
        }
       
    }
    void Vibrate()
    {
        Handheld.Vibrate();
    }
    public void LevelComplete()
    {
        TouchMovement.instance.clutch=true;
        //assining the new max_level not assigned if we are repeating the level
        if(GameManager.instance.levelno>GameManager.instance.current_max_level)
        {
            GameManager.instance.current_max_level+=1;
        }
        int playerNo=(int)double.Parse(playerPointText.text);
        //star points
        int starPoints=0,totalExpPoints=0;
        //adding the expoints 
        if(playerNo>GameManager.instance.objNos[0])
        {
            GameManager.instance.ExpPoints+=GameManager.instance.rewardXpPoints[0];
            //assingign the individual text field
            
            totalExpPoints+=GameManager.instance.rewardXpPoints[0];
            starPoints+=1;
        }
        if(playerNo>GameManager.instance.objNos[1])
        {
            GameManager.instance.ExpPoints+=GameManager.instance.rewardXpPoints[1];
            
            totalExpPoints+=GameManager.instance.rewardXpPoints[1];
            starPoints+=1;
        }
        if(playerNo>GameManager.instance.objNos[2])
        {
            GameManager.instance.ExpPoints+=GameManager.instance.rewardXpPoints[2];
            
            totalExpPoints+=GameManager.instance.rewardXpPoints[2];
            starPoints+=1;
        }
        for(var i=0;i<indi_expPoints.Length;i++)
        {
            indi_expPoints[i].text="+"+GameManager.instance.rewardXpPoints[i]+"xp";
        }
        //assinging the level bonus maybe animate it with the objectives?
        level_complete_panel_xp_points.text="+"+totalExpPoints+"xp";
        
        GameManager.instance.stars_in_level[GameManager.instance.levelno-1]
        =starPoints;
        //setting the star icons
        for(int i=0;i<starPoints;i++)
        {
            star[i].color=Color.white;
            checks[i].gameObject.SetActive(true);
        }
        for(int i=starPoints;i<3;i++)
        {
            star[i].color=gray;
            checks[i].gameObject.SetActive(false);
        }
        //setting the 
        for(int i=0;i<3;i++)
        {
            objectives[i].text=GameManager.instance.objPoints[i];
        }
        levelComplete.SetActive(true);

    }
    IEnumerator playerDestruction()
    {
        yield return new WaitForSeconds(0.08f);
        Time.timeScale=0.01f;
        StartCoroutine(gameOverSequence());
    }
    IEnumerator gameOverSequence()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        Time.timeScale=0f;
        watch_ad_panel.SetActive(true);
        CancelInvoke();
    }

}
