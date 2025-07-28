using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public struct SpawnCoordinates
{
    public Transform[] innerCoordinates,outerCoordnates;
}
public class EnemySpawner : MonoBehaviour
{
    //prefabs
    public GameObject NumberEnemyPrefab, NumberFriendPrefab;
    public GameObject gameSceneChanger;
    //quadrants of the spawnPoint
    public SpawnCoordinates UpperQuadrant,DownQuadrant;
    
    //time variables
    
    public float timeIntervalInWhichTheFriendlySceneIsChanged;//constant
    public float timePassedSinceLastChangeInScene;//this is used to keep track of the time in the game so that the game changer can be initialized
    public float timePassedSinceLastSpawn;//this is used to keep track of the time between spawns 
    public float timeIntervalInDifferentSpawnForFriendlyNumbers;//constant time which changes with each game scene change
    public float timeIntervalInDifferentSpawnForEnemyNumbers;//constant
    public float timeIntervalInWhichTheEnemySceneIsChanged;//constant

    public SpriteRenderer sprite;

    public float speedOfTheGameChanger ;
    public Vector3 StartPointOfTheGameSceneChanger;
    public float yMarginForTheGameSceneChamger;
    private int changeInSceneCounter;
    
    //to check whether the game scene changer is moving or not
    private bool moving;
   

    //temp gameObjects
    [HideInInspector]
    public GameObject tempGameSceneChanger;

    //list of active numbers
    public List<GameObject> listOfFriendlyNumbers = new List<GameObject>();
    public List<GameObject> listOfEnemyNumbers = new List<GameObject>();

    //bool to check which type of number to spawn
    [HideInInspector]public bool friend;
    
    //speed of the spwaned numbers
    private float initialSpeedOfTheFriendlyNumbers;
    private float speedOfTheFriendlyNumbersInAGamePlay;
    private float initialSpeedOfTheEnemyNumbers;
    private float speedOfTheEnemyNumbersInAGamePlay;

    //array with separate numbers
    private int[] arrayOfSeparateNosInEachGameScene;
    private int[] arrayOfScaleNos;


    //total no of number entiies in the screen
    private int totalNoOfEntities;
    //bool to check if to bring down the gameChanger
    private bool bringdown;
    private IEnumerator bringingdown;

    //total No of the enemies to be spawned
    private int tempTotal;
    //used for setting the cover numbers
    private int multiplier;

    //used in bringing down the game_changer
    private float time_game_changer;

    private bool playedOnce;
    public static EnemySpawner instance;

    public GameObject sceneTimer;
    public TextMeshProUGUI timer_text_scene_counter;

    float timer_value;
    private void Awake()
    {
        MakeInstance();
        //setting the spawn_lag
        timeIntervalInDifferentSpawnForFriendlyNumbers-=GameManager.instance.decrement_in_spawn_time;
        timeIntervalInDifferentSpawnForEnemyNumbers=GameManager.instance.curr_enemy_spawn_interval;
        //setting the speed
        initialSpeedOfTheFriendlyNumbers = 1f;
        totalNoOfEntities = 0;
        speedOfTheFriendlyNumbersInAGamePlay = initialSpeedOfTheFriendlyNumbers;
        initialSpeedOfTheEnemyNumbers = 3f;
        speedOfTheEnemyNumbersInAGamePlay = initialSpeedOfTheEnemyNumbers;

        //setting the bool identifier for the type orf gamneplay
        friend = !friend;

        
        //setting the time variables
        timePassedSinceLastChangeInScene = 0f;
        timePassedSinceLastSpawn = 0f;

        

        //instantiating the gamescenechanger
        tempGameSceneChanger = Instantiate(gameSceneChanger, StartPointOfTheGameSceneChanger, Quaternion.identity);
        
        //chanmging the color of the game scene changer and its sparks
        GameColorUnification.instance.ChangeTheSelectedColor(!friend);
        tempGameSceneChanger.GetComponent<GameChangerSparksChangeColor>().changeTheColorOfTheSparks
        (GameColorUnification.instance.selectedColor);
        
        tempGameSceneChanger.SetActive(false);

       
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
        //providing all the to be spawned nos with a number
        if (friend == true)
        {
            tempTotal =(int) (timeIntervalInWhichTheFriendlySceneIsChanged / timeIntervalInDifferentSpawnForFriendlyNumbers);
            arrayOfSeparateNosInEachGameScene = NumberProvider.instance.numberProviderForEachScene(friend, tempTotal);
            arrayOfScaleNos= NumberProvider.instance.scaleProviderForTheNumbers(tempTotal,arrayOfSeparateNosInEachGameScene);
                       
        }
        else
        {
            tempTotal = (int)(timeIntervalInWhichTheEnemySceneIsChanged / timeIntervalInDifferentSpawnForEnemyNumbers);
            Debug.Log(tempTotal);
            arrayOfSeparateNosInEachGameScene = NumberProvider.instance.numberProviderForEachScene(friend, tempTotal);
            arrayOfScaleNos = NumberProvider.instance.scaleProviderForTheNumbers(tempTotal, arrayOfSeparateNosInEachGameScene);
            
            
        }
        multiplier=tempTotal;

        //spawning the first number
        if (totalNoOfEntities < tempTotal)
        {
           
            if(GameManager.instance.tutDone)
            {
                //if statement to insure that under no circumstance the index gets out of range
                SpawnTheNumber(friend, totalNoOfEntities);

            }
        }
        if(friend)
        {
            time_game_changer=17f;
        }
        else
        {
            time_game_changer=8f;
        }
        if(!GameManager.instance.InTutorial)
        {
            sceneTimer.SetActive(true);
            timer_value=timeIntervalInWhichTheFriendlySceneIsChanged+time_game_changer;
            timer_text_scene_counter.text=timer_value.ToString("F0")+"s";
        }

    }

    public void SpawnNumberFromTutorialManager(bool f,bool c=false)
    {
        SpawnTheNumber(f,totalNoOfEntities,c);
    }
    //used to spawn the number
    public void SpawnTheNumber(bool friend,int index,bool cover=false)
    {
        GameObject tempNumber;
        if(GameManager.instance.InTutorial)
        {
            float _x=0,_y=0;
            if(friend)
            {
                if(cover)
                {
                    AssignTheCoordinates(ref _x,ref _y,4);
                    tempNumber = Instantiate(NumberFriendPrefab, new Vector3(_x, _y, 0f), Quaternion.identity);
                    
                    //changing the name so that the name could be unique
                    tempNumber.name=index.ToString();

                    tempNumber.GetComponent<MovementOfTheNumbers>().speed = speedOfTheFriendlyNumbersInAGamePlay;
                    //assigning the number&scale to each entity
                    tempNumber.transform.localScale = new Vector3(4f, 4f, 0f);
                    tempNumber.GetComponent<IndividualNumberProvider>().realNumberForTheFriend=20+"";
                    tempNumber.GetComponent<IndividualNumberProvider>().AssisgnTheCoverNumber("3");
                    tempNumber.GetComponent<NumberBehaviour>().orderColorChange();
                }
                else
                {
                    AssignTheCoordinates(ref _x,ref _y,3);

                    tempNumber = Instantiate(NumberFriendPrefab, new Vector3(_x, _y, 0f), Quaternion.identity);
                    
                    //changing the name so that the name could be unique
                    tempNumber.name=index.ToString();

                    tempNumber.GetComponent<MovementOfTheNumbers>().speed = speedOfTheFriendlyNumbersInAGamePlay;
                    //assigning the number&scale to each entity
                    tempNumber.transform.localScale = new Vector3(3f, 3f, 0f);
                    tempNumber.GetComponent<IndividualNumberProvider>().realNumberForTheFriend="20";
                    tempNumber.GetComponent<IndividualNumberProvider>().AssisgnTheNumber("NA");
                    tempNumber.GetComponent<NumberBehaviour>().orderColorChange();
                }
            }
            else
            {
                AssignTheCoordinates(ref _x,ref _y,4);
                tempNumber = Instantiate(NumberEnemyPrefab, new Vector3(_x, _y, 0f), Quaternion.identity);
                
                //changing the name so that the name could be unique
                tempNumber.name=index.ToString();
                
                tempNumber.GetComponent<MovementOfTheNumbers>().speed = speedOfTheEnemyNumbersInAGamePlay;

                //assigning the number to each entity
                tempNumber.GetComponent<IndividualNumberProvider>().AssisgnTheNumber("3");
                tempNumber.transform.localScale = new Vector3(4f, 4f, 0f);

            }
        }
        else
        {
            speedOfTheFriendlyNumbersInAGamePlay += 0.3f;
            
            float tempSpeedOfFriendlyNumbers = speedOfTheFriendlyNumbersInAGamePlay;
            //setting the co ordinates of the number
            float yAxis=0f;
            float xAxis=0f;
            AssignTheCoordinates(ref xAxis,ref yAxis,arrayOfScaleNos[index]);
            if (friend == true)
            {
                tempNumber = Instantiate(NumberFriendPrefab, new Vector3(xAxis, yAxis, 0f), Quaternion.identity);
                
                //changing the name so that the name could be unique
                tempNumber.name=index.ToString();

                tempNumber.GetComponent<MovementOfTheNumbers>().speed = tempSpeedOfFriendlyNumbers;
                //assigning the number&scale to each entity
                tempNumber.transform.localScale = new Vector3(arrayOfScaleNos[index], arrayOfScaleNos[index], 0f);
                if(tempNumber.transform.localScale.x<=3f)
                {
                    //if the no is small in scale there is no need for any cover
                    tempNumber.GetComponent<IndividualNumberProvider>().realNumberForTheFriend=arrayOfSeparateNosInEachGameScene[index].ToString();
                    tempNumber.GetComponent<IndividualNumberProvider>().AssisgnTheNumber("NA");
                }
                else
                {
                    //if the no is big then the cover is needed
                    int coverNo=tempNumber.GetComponent<IndividualNumberProvider>().returnCoverNumber(multiplier);
                    multiplier-=1;
                
                    tempNumber.GetComponent<IndividualNumberProvider>().realNumberForTheFriend=arrayOfSeparateNosInEachGameScene[index].ToString();
                    tempNumber.GetComponent<IndividualNumberProvider>().AssisgnTheCoverNumber(coverNo.ToString());
                }
                //changing the trail properties of the spawned numbers
                tempNumber.GetComponent<NumberBehaviour>().orderColorChange();
                listOfFriendlyNumbers.Add(tempNumber);

            }
            else
            {
                tempNumber = Instantiate(NumberEnemyPrefab, new Vector3(xAxis, yAxis, 0f), Quaternion.identity);
                
                //changing the name so that the name could be unique
                tempNumber.name=index.ToString();
                
                tempNumber.GetComponent<MovementOfTheNumbers>().speed = speedOfTheEnemyNumbersInAGamePlay;

                //assigning the number to each entity
                tempNumber.GetComponent<IndividualNumberProvider>().AssisgnTheNumber(arrayOfSeparateNosInEachGameScene[index].ToString());
                tempNumber.transform.localScale = new Vector3(arrayOfScaleNos[index], arrayOfScaleNos[index], 0f);

                listOfEnemyNumbers.Add(tempNumber);
            }

            //Keeping the track of the total no of entities so that we can get the index
            totalNoOfEntities += 1;

        }

    }//spawn the Number

    void AssignTheCoordinates(ref float xAxis,ref float yAxis, float scale)
    {
        int upOrDown=Random.Range(1,3);
        int innerOrOuter=Random.Range(1,3);
        int randomSpawn=Random.Range(0,3);
        if(scale>3f)
        {
            if(upOrDown==1)
            {
                xAxis=UpperQuadrant.innerCoordinates[randomSpawn].position.x;
                yAxis=UpperQuadrant.innerCoordinates[randomSpawn].position.y;

            }
            else
            {
                xAxis=DownQuadrant.innerCoordinates[randomSpawn].position.x;
                yAxis=DownQuadrant.innerCoordinates[randomSpawn].position.y;
            }
        }
        else
        {
            if(upOrDown==1)
            {
                if(innerOrOuter==1)
                {
                    xAxis=UpperQuadrant.innerCoordinates[randomSpawn].position.x;
                    yAxis=UpperQuadrant.innerCoordinates[randomSpawn].position.y;
                }
                else
                {
                    xAxis=UpperQuadrant.outerCoordnates[randomSpawn].position.x;
                    yAxis=UpperQuadrant.outerCoordnates[randomSpawn].position.y;
                }
            }
            else
            {
                if(innerOrOuter==1)
                {
                    xAxis=DownQuadrant.innerCoordinates[randomSpawn].position.x;
                    yAxis=DownQuadrant.innerCoordinates[randomSpawn].position.y;
                }
                else
                {
                    xAxis=DownQuadrant.outerCoordnates[randomSpawn].position.x;
                    yAxis=DownQuadrant.innerCoordinates[randomSpawn].position.y;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!GameManager.instance.InTutorial)
        {
            if(timer_value>=0)
            {
                timer_value-=Time.deltaTime;
                timer_text_scene_counter.text=timer_value.ToString("F0")+"s";
            }
            //updating the time variables
            if (moving == false)
            {
                timePassedSinceLastChangeInScene += Time.deltaTime;
               
                timePassedSinceLastSpawn += Time.deltaTime;
            }

            //checking if the time has come for the gameplayChange
            if(timePassedSinceLastChangeInScene>timeIntervalInWhichTheFriendlySceneIsChanged&&friend==true)
            {

                timePassedSinceLastChangeInScene = 0f;

                //this statement is used to call the gamechanger scene
                moving = true;
            }
            if(timePassedSinceLastChangeInScene>timeIntervalInWhichTheEnemySceneIsChanged&&friend==false)
            {

                timePassedSinceLastChangeInScene = 0f;

                //this statement is used to call the gamechanger scene
                moving = true;

            }

            //checking if  the time has come for another spawn
            if (timePassedSinceLastSpawn>timeIntervalInDifferentSpawnForFriendlyNumbers&&friend==true)
            {

                
                timePassedSinceLastSpawn=0f;
                if (totalNoOfEntities < tempTotal)
                {
                    //if statement to insure that under no circumstance the index gets out of range
                    SpawnTheNumber(friend, totalNoOfEntities);
                }
            }
            if(timePassedSinceLastSpawn>timeIntervalInDifferentSpawnForEnemyNumbers&&friend==false)
            {
                
                timePassedSinceLastSpawn = 0f;
                if (totalNoOfEntities < tempTotal)
                {
                    //if statement to insure that under no circumstance the index gets out of range
                    SpawnTheNumber(friend, totalNoOfEntities);
                }
                
            }

            //checking if the gamechangerprefab has reached the bottom
            if (tempGameSceneChanger.transform.position.y<yMarginForTheGameSceneChamger)
            {
                //change in gameplay is initialized

                //update the scene counter here
                if(!friend)
                {
                    SceneCounter.instance.UpdateTheCounter();
                    changeInSceneCounter += 1;//changing the scenecounter

                }
                DestroyAndInstantiateTheGameSceneChanger();
            }
            else
            {
                if(moving==true)
                {
                    if(bringingdown==null)
                    {

                        bringingdown = Coroutinebringingdown();
                        StartCoroutine(bringingdown);
                    }
                    if(bringdown)
                    {
                        BringIntheGameSceneChanger();

                    }
                    
                }
            }

        }

    }//update
    //wait for 5 sec before bringing down the gamechanger
    IEnumerator Coroutinebringingdown()
    {
        yield return new WaitForSeconds(time_game_changer);
        
        bringdown = true;
        
        
    }
    void BringIntheGameSceneChanger()
    {
       
        tempGameSceneChanger.SetActive(true);
        if(!playedOnce)
        {
            tempGameSceneChanger.GetComponents<AudioSource>()[0].PlayOneShot(AudioManager.instance.gameChanger,AudioManager.instance.volume);
            playedOnce=true;
        }
        //tempGameSceneChanger.transform.Translate(Vector3.down*Time.deltaTime*speedOfTheGameChanger);
    }//bring in the game scene changer

    void DestroyAndInstantiateTheGameSceneChanger()
    {
        //check here for the sceneCounter and the total no of scenes and command a win
        if(changeInSceneCounter==GameManager.instance.curr_scene_counter)
        {
            Debug.Log("Win");
            
            Time.timeScale=0f;
            GameConclusionScript.instance.LevelComplete();
        }
        
        bringingdown = null;
        bringdown = false;
        moving = false;
        playedOnce=false;
        //changing the color of the scene according to the game scene changer's color
        GameColorUnification.instance.ChangeTheColorOfTheBullets();
        GameColorUnification.instance.ChangeTheColorOfTheNonColliderObjects();
        Destroy(tempGameSceneChanger);

        //changing the gameplay
        UpdateTheTimeFriendAndNoOfNumbersAndSpeed();
        
        //upating the time intervals 
        sceneTimer.SetActive(true);
        if(friend)
            timer_value=timeIntervalInWhichTheFriendlySceneIsChanged+time_game_changer;
        else
            timer_value=timeIntervalInWhichTheEnemySceneIsChanged+time_game_changer;
        timer_text_scene_counter.text=timer_value.ToString("F0")+"s";
        //destroying all the numbers(temporary jugaad)
        DestroyAllthePresentNumbers();

        //time passed since last spawn should be equal to 0 
        timePassedSinceLastSpawn = 0f;

        tempGameSceneChanger = Instantiate(gameSceneChanger, StartPointOfTheGameSceneChanger, Quaternion.identity);

        //chanmging the color of the game scene changer and its sparks
        GameColorUnification.instance.ChangeTheSelectedColor(!friend);
        tempGameSceneChanger.GetComponent<GameChangerSparksChangeColor>().changeTheColorOfTheSparks
        
        (GameColorUnification.instance.selectedColor);

        tempGameSceneChanger.SetActive(false);

        //providing all the to be spawned nos with a number
        if (friend == true)
        {
            tempTotal = (int)(timeIntervalInWhichTheFriendlySceneIsChanged / timeIntervalInDifferentSpawnForFriendlyNumbers);
            arrayOfSeparateNosInEachGameScene = NumberProvider.instance.numberProviderForEachScene(friend, tempTotal);
            arrayOfScaleNos = NumberProvider.instance.scaleProviderForTheNumbers(tempTotal, arrayOfSeparateNosInEachGameScene);
           
            //maintaining the chromatic abbration
            PostProcessingScript.instance.ToggleChromaticAbrassion(true);

           //changing the background music
           AudioManager.instance.InFriendlyGamePlay();
        }
        else
        {
            tempTotal = (int)(timeIntervalInWhichTheEnemySceneIsChanged / timeIntervalInDifferentSpawnForEnemyNumbers);
            Debug.Log(tempTotal);
            arrayOfSeparateNosInEachGameScene = NumberProvider.instance.numberProviderForEachScene(friend, tempTotal);
            arrayOfScaleNos = NumberProvider.instance.scaleProviderForTheNumbers(tempTotal, arrayOfSeparateNosInEachGameScene);
            //assiging temp total when the enemyscene comes this would be used in spawning the ammo packs for the players
            CoinPoweupSpawner.instance.AssignTheTotalEntities(tempTotal);
            
            //maintaining the chromatic abbration
            PostProcessingScript.instance.ToggleChromaticAbrassion(false);


            //changing the audio in the background in the enemy scene
            AudioManager.instance.InEnemyGamePlay();
        }
        multiplier=tempTotal;
        //resetting the totalNoOFEntites
        totalNoOfEntities = 0;


    }//destroy the game scene changer

    void DestroyAllthePresentNumbers()
    {
        for(int i=0;i<listOfEnemyNumbers.Count;i++)
        {
            Destroy(listOfEnemyNumbers[i]);
        }
        listOfEnemyNumbers.Clear();
        for (int i = 0; i < listOfFriendlyNumbers.Count; i++)
        {
            Destroy(listOfFriendlyNumbers[i]);
        }
        listOfFriendlyNumbers.Clear();
        listOfEnemyNumbers.Clear();

    }//destroy the present Numbera

    void UpdateTheTimeFriendAndNoOfNumbersAndSpeed()
    {
        //changing the gameplayt a bit

        //speed variables
        initialSpeedOfTheFriendlyNumbers += 0.5f;
        speedOfTheFriendlyNumbersInAGamePlay = initialSpeedOfTheFriendlyNumbers;
        initialSpeedOfTheEnemyNumbers += 0.1f;
        speedOfTheEnemyNumbersInAGamePlay = initialSpeedOfTheEnemyNumbers;

        friend = !friend;
        if(friend)
        {
            time_game_changer=17f;
        }
        else
        {
            time_game_changer=8f;
        }
    }
}
