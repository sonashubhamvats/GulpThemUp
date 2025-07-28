using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct selectedItemInfo
{
    public Sprite icon;

    public ItemName name;

    public int cost;

    public int quantity;
}
public class GameManager : MonoBehaviour
{
    public float[] offsetCannons=new float[7];
    public Vector3 offsetForCannon;
    //level counter for showing ads
    public int levelCounter_ad;

    //the variables still need to be saved
     //the variables end here
    public int InputType=0;

    [Range(0.1f,1f)]
    public float joystickSens;
    
    [Range(0.1f,1f)]
    public float touchSens;

    [Range(0f,1f)]
    public float volume;
    
    public int current_max_level;

    public int[] stars_in_level=new int[50];
   

    //congrats bool
    public bool[] congrats=new bool[9];
    
    //rank no variable
    public int rank;

    public Image progressBar;

    public GameObject loadingScene;
    public int xp_type;


    //the current sprites and material of the player and the cannon
    public Sprite playerSprite,cannonSprite;

    public Material playerMaterial,cannonMaterial;

    
    //the cooldown and the shieldtime in the game 
    public int shieldTime,cooldown;
    //guntyoe which depictsa the type of bullets
    public int gun_type;
    
    //bool variables for the player and the cannon selection
    public bool[] bought_player=new bool[7];

    public bool[] selected_player=new bool[7];

    public bool[] bought_cannon=new bool[7];

    public bool[] selected_cannon=new bool[7];

    

    public Material[] _player_material,_cannon_material;

    public Sprite[] _player_sprites,_cannon_sprites;

    public List<selectedItemInfo> listOfSelectedItems=new List<selectedItemInfo>();
    public static GameManager instance;
    [HideInInspector]
    public int ExpPoints,CoinCount;

    [HideInInspector]public int noOfScenes=3,LevelTotal=51;

    public int[] scene_counter={3,3,4,4,4,5,5,5,6,6};
    public int curr_scene_counter;
    
    //damage variables
    [HideInInspector]public float normalDamagefFriend,normalDamageEnemy;
    
    [HideInInspector]public int inReflectiveDamage,inMissileDamage;
    
    //damage variables ends

    //damage arrays
    [HideInInspector]public float[] friendDamageN={0.1f,0.2f,0.3f,0.4f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f}
    ,enemyDamageN={0.6f,0.6f,0.6f,0.7f,0.7f,0.7f,0.7f,0.7f,0.7f,0.7f};

    [HideInInspector]public int[]  reflectiveDamage={5,5,5,5,5,6,7,8,9,10};

    [HideInInspector]public int[] missileDamage={30,30,30,30,30,30,30,30,40,50};

    [HideInInspector]public int[] enemySpawnInterval={3,3,3,3,3,3,2,2,2,2};

    [HideInInspector]public int curr_enemy_spawn_interval;

    //damage arrays ends

    //number cover variables
    [HideInInspector]public int totalNoOnTheCover;

    //ammo for the bullets
    [HideInInspector]public int normalCount,PiercingCount,reflectiveCount,MissileCount;

    //total no of bullet types
    [HideInInspector]public int types_of_bullets;
 
    [HideInInspector]public float decrement_in_spawn_time;

    private BulletCountProvider bulletCountProvider;

    public int stalled_level_no;

   
    public int levelno;
    
    private GameData gameData;
    
    private TextMeshProUGUI Rank;
    private XpRankManager xpRankManager;
    public bool tutorialDone;

    //should be toggled by the button here it is by default-
    public bool InTutorial=false;

    public bool tutDone;


    //temp Objective Manager variables
    public int[] rewardXpPoints=new int[3];

    public int[] objNos=new int[3];
    public string[] objPoints=new string[3]; 
    public GameObject tutorialPromptStart;
    public GameObject tutorialPoint;
    private void Awake()
    {
        //check for the rank here this function called at the very start of the scene MainMenu
        //the start in some other script is assiging the rank to the box cause
        
        MakeInstance();
        InitializeGameData();
        
       
        
        tutDone=false;
        
        
    }
    private void Start()
    {
        levelCounter_ad=0;
    }

    void InitializeGameData()
    {
        LoadGameData();
        if(gameData==null)
        {
            //firsttime   
            tutorialDone=false; 
            Debug.Log("First time");
            tutorialPromptStart.SetActive(true);
            tutorialPoint.SetActive(true);
            //here show the message
            ExpPoints=0;
            CoinCount=0;
            bought_cannon[0]=true;
            bought_player[0]=true;
            selected_cannon[0]=true;
            selected_player[0]=true;
            shieldTime=3;
            cooldown=11;
            gun_type=1;
            rank=1;
            InputType=0;
            joystickSens=0.5f;
            touchSens=0.5f;
            volume=1f;
            current_max_level=0;

            gameData=new GameData();
            gameData._shielTime=3;
            gameData._cooldown=11;
            gameData._tutorialDone=tutorialDone;
            gameData._guntype=1;
            gameData._rank=rank;
            gameData._current_max_level=current_max_level;
            gameData._input_type=InputType;
            gameData._joySens=joystickSens;
            gameData._touchSens=touchSens;
            gameData._volume=volume;
            gameData._star_count=stars_in_level;
            gameData._congrats=congrats;

            gameData._selected_player=selected_player;
            gameData._selected_cannon=selected_cannon;
            gameData._bought_cannon=bought_cannon;
            gameData._boughtPlayer=bought_player;
            
            gameData._exppoint=0;
            gameData._coin_count=0;
            SaveGameData();
        }

    }
    void MakeInstance()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
    void SaveGameData()
    {
        FileStream file=null;
        try
        {
            BinaryFormatter binaryFormatter=new BinaryFormatter();
            file=File.Create(Application.persistentDataPath+"alphabuild_final.dat");
            if(gameData!=null)
            {
                gameData._tutorialDone=tutorialDone;
                gameData._shielTime=shieldTime;
                gameData._cooldown=cooldown;
                gameData._guntype=gun_type;
                gameData._rank=rank;
                gameData._current_max_level=current_max_level;
                gameData._star_count=stars_in_level;
                gameData._input_type=InputType;
                gameData._joySens=joystickSens;
                gameData._touchSens=touchSens;
                gameData._volume=volume;

                gameData._congrats=congrats;
                gameData._selected_player=selected_player;
                gameData._selected_cannon=selected_cannon;
                gameData._bought_cannon=bought_cannon;
                gameData._boughtPlayer=bought_player;

                gameData._exppoint=ExpPoints;
                gameData._coin_count=CoinCount;

                binaryFormatter.Serialize(file,gameData);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            file.Close();
        }
    }
    void LoadGameData()
    {
        FileStream file=null;
        try
        {
            BinaryFormatter binaryFormatter=new BinaryFormatter();
            file=File.Open(Application.persistentDataPath+"alphabuild_final.dat",FileMode.Open);
            gameData=(GameData)binaryFormatter.Deserialize(file);
            if(gameObject!=null)
            {
                tutorialDone=gameData._tutorialDone;
                shieldTime=gameData._shielTime;
                cooldown=gameData._cooldown;
                gun_type=gameData._guntype;
                rank=gameData._rank;
                current_max_level=gameData._current_max_level;
                stars_in_level=gameData._star_count;
                congrats=gameData._congrats;
                InputType=gameData._input_type;
                joystickSens=gameData._joySens;
                touchSens=gameData._touchSens;
                volume=gameData._volume;


                selected_player=gameData._selected_player;
                selected_cannon=gameData._selected_cannon;
                bought_cannon=gameData._bought_cannon;
                bought_player=gameData._boughtPlayer;

                ExpPoints=gameData._exppoint;
                CoinCount=gameData._coin_count;
            }

        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
        finally
        {
            if(file!=null)
            {
                file.Close();
            }
        }
    }
    private void OnApplicationPause(bool pause)
    {
        
        if(pause==true)
        {
            SaveGameData();
        }
    }
    private void OnApplicationQuit() {
        
        SaveGameData();
    }
    public void startGame(int levelNo)
    {
        stalled_level_no=levelNo;
        
        ButtonManager.instance.OpenStartPanel();

    }
    //saving the progress ends here

    public void InitiateStartGame()
    {
        //updating the stats for the game
        WritingInTheStatsForTheLevel(stalled_level_no);
        //SceneManager.LoadScene("GamePlay");
        ButtonManager.instance.StartGameWithLoadingScreen();

    }
    void WritingInTheStatsForTheLevel(int levelNo)
    {

        //getting the levelno-
        levelno=levelNo;
        //enabling the start conditions that will change how the different levels spawn numbers
        LevelTotal=51+(levelNo-1)*50;
        Debug.Log("level-  "+levelNo);
        int increment=levelNo/5;
        if(levelNo%5==0&&increment!=0)
        {
        
            increment--;
        }
        //assiging the xp type
        int tempXP=levelNo/10;
        tempXP++;
        if(levelNo%10==0)
        {
            tempXP--;
        }
        xp_type=tempXP;

        Debug.Log(increment);
        //setting the new decrement in the spaentime
        decrement_in_spawn_time=(increment/10f);
        Debug.Log("decrement- "+decrement_in_spawn_time+"increment- "+increment);
        
        noOfScenes=3+increment;
        curr_scene_counter=scene_counter[increment];

        float damageNF,damageNE;
        int damageR,damageM;
        //when the rank system has been implemented turn this system which uses increment into a system which uses rank
        curr_enemy_spawn_interval=enemySpawnInterval[increment];
        damageNE=enemyDamageN[rank-1];
        damageNF=friendDamageN[rank-1];
        damageR=reflectiveDamage[rank-1];
        damageM=missileDamage[rank-1];
        
        
        normalDamageEnemy=damageNE;
        normalDamagefFriend=damageNF;
        
        bulletCountProvider=new BulletCountProvider();
        
        if(increment>=2&&increment<=3)
        {
            //pierce
            bulletCountProvider.provideAmmoAndCoverTotal(levelNo,damageNF,(damageNF+0.3f));
            types_of_bullets=gun_type;
        }
        else if(increment>=4&&increment<=6)
        {            
            //relfective,pierce
            bulletCountProvider.provideAmmoAndCoverTotal(levelNo,damageNF,(damageNF+0.3f),damageR);
            //has to be shifted to the bullet count provider
            types_of_bullets=gun_type;
        }
        else if(increment>=7&&increment<=9)
        {
            //relfective ,pierce , missile
            bulletCountProvider.provideAmmoAndCoverTotal(levelNo,damageNF,(damageNF+0.3f),damageR,damageM);
            types_of_bullets=gun_type;
        }
        else
        {
            //only normal
            bulletCountProvider.provideAmmoAndCoverTotal(levelNo,damageNF);
            Debug.Log("only normal");
            types_of_bullets=gun_type;
        }
        switch(gun_type)
        {
            case 3:
                inReflectiveDamage=damageR;
                break;
            case 4:
                inReflectiveDamage=damageR;
                inMissileDamage=damageM;
                break;
        }
        //assinging the current player and cannon sprites
        for(int i=0;i<selected_cannon.Length;i++)
        {
            if(selected_cannon[i])
            {
                cannonMaterial=_cannon_material[i];
                cannonSprite=_cannon_sprites[i];
                offsetForCannon.y=offsetCannons[i];
            }
            if(selected_player[i])
            {
                playerMaterial=_player_material[i];
                playerSprite=_player_sprites[i];
            }
        }
    }
}
