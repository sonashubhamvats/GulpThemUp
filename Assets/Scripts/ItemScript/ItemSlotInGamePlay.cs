using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlotInGamePlay : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [HideInInspector]public ItemName nameOfTheItemInTheSlot;

    public TextMeshProUGUI textOnItem;

    public GameObject ammo_prefab;

    [HideInInspector]public int count;

    private float temp_countDown;
    public GameObject[] InitSequence;

    [HideInInspector]public bool locked;

    public GameObject[] otherTwo;

    private float waitForSeconds;

    [HideInInspector]public bool loading;
    
    private void Awake()
    {
        
    }
    public void UpdateTheValuesOfTheSlot(ItemName item_name,int quantity,Sprite icon)
    {
        
        count=quantity;
        nameOfTheItemInTheSlot=item_name;
        GetComponent<Image>().sprite=icon;
        var tempcolo=GetComponent<Image>().color;
        tempcolo.a=1f;
        GetComponent<Image>().color=tempcolo;
        textOnItem.gameObject.SetActive(true);
        textOnItem.text="";
        if(count>1)
        {
            GetComponent<Image>().color=Color.gray;
            var tempcolor=GetComponent<Image>().color;
            tempcolor.a=0.8f;
            GetComponent<Image>().color=tempcolor;
            
            textOnItem.text=""+count;
        }
        switch(nameOfTheItemInTheSlot)
        {
            case ItemName._2xMultiplier:
                waitForSeconds=4f;
                break;
            case ItemName._3xMultiplier:
                waitForSeconds=4f;
                break;
            case ItemName.GunsOfRetribution:
                waitForSeconds=24f;
                break;
            case ItemName.RainOfLife:
                waitForSeconds=16f;
                break;
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(!locked&&!loading)
        {
            AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.mm_normal_button_click,
            AudioManager.instance.volume);
            if(GetComponent<Image>().color.a>0f)
            {
                count--;
                if(count==1)
                {
                    textOnItem.text="";    
                    var t1=GetComponent<Image>().color;
                    t1=Color.white;
                    t1.a=1f;
                    
                    GetComponent<Image>().color=t1;

                }
                else if(count>1)
                {
                    textOnItem.text=""+count;
                }
                else if(count==0)
                {
                    var t2=GetComponent<Image>().color;
                    t2.a=0f;
                    GetComponent<Image>().color=t2;
                    locked=true;

                }
                switch(nameOfTheItemInTheSlot)
                {
                    case ItemName._2xMultiplier:
                        locked=true;
                        loading=true;
                        
                        //setting the countdown for the multiplier
                        textOnItem.text=""+15;
                        temp_countDown=15f;
                        
                        GetComponent<Image>().color=Color.gray;
                        var tempcolor=GetComponent<Image>().color;
                        tempcolor.a=0.8f;
                        GetComponent<Image>().color=tempcolor;

                        //initiating the cooldown
                        InitiateTheCooldown(waitForSeconds);

                        for(int i=0;i<2;i++)
                        {
                            
                            if(otherTwo[i].GetComponent<ItemSlotInGamePlay>().count>0)
                            {
                                otherTwo[i].GetComponent<ItemSlotInGamePlay>().locked=true;
                                otherTwo[i].GetComponent<Image>().color=Color.gray;
                                tempcolor=otherTwo[i].GetComponent<Image>().color;
                                tempcolor.a=0.8f;
                                otherTwo[i].GetComponent<Image>().color=tempcolor;
                            }

                        }

                        for(int i=0;i<InitSequence.Length;i++)
                        {
                            if(InitSequence[i].name=="2xInit")
                            {
                                InitSequence[i].SetActive(true);
                            }
                        }
                        break;
                    case ItemName._3xMultiplier:
                        locked=true;
                        loading=true;
                        
                        //setting the countdown for the multiplier
                        textOnItem.text=""+15;
                        temp_countDown=15f;
                        
                        GetComponent<Image>().color=Color.gray;
                        tempcolor=GetComponent<Image>().color;
                        tempcolor.a=0.8f;
                        GetComponent<Image>().color=tempcolor;
                        //initiating the cooldown
                        InitiateTheCooldown(waitForSeconds);

                        for(int i=0;i<2;i++)
                        {
                            
                            if(otherTwo[i].GetComponent<ItemSlotInGamePlay>().count>0)
                            {
                                otherTwo[i].GetComponent<ItemSlotInGamePlay>().locked=true;
                                otherTwo[i].GetComponent<Image>().color=Color.gray;
                                tempcolor=otherTwo[i].GetComponent<Image>().color;
                                tempcolor.a=0.8f;
                                otherTwo[i].GetComponent<Image>().color=tempcolor;
                            }

                        }

                        for(int i=0;i<InitSequence.Length;i++)
                        {
                            if(InitSequence[i].name=="3xInit")
                            {
                                InitSequence[i].SetActive(true);
                            }
                        }
                        break;
                    case ItemName.RainOfLife:
                        locked=true;
                        //initiating the cooldown
                        InitiateTheCooldown(waitForSeconds);
                        if(count>0)
                        {
                            GetComponent<Image>().color=Color.gray;
                            tempcolor=GetComponent<Image>().color;
                            tempcolor.a=0.8f;
                            GetComponent<Image>().color=tempcolor;
                        }
                        for(int i=0;i<2;i++)
                        {
                            
                            if(otherTwo[i].GetComponent<ItemSlotInGamePlay>().count>0)
                            {
                                otherTwo[i].GetComponent<ItemSlotInGamePlay>().locked=true;
                                otherTwo[i].GetComponent<Image>().color=Color.gray;
                                tempcolor=otherTwo[i].GetComponent<Image>().color;
                                tempcolor.a=0.8f;
                                otherTwo[i].GetComponent<Image>().color=tempcolor;
                            }

                        }

                        for(int i=0;i<InitSequence.Length;i++)
                        {
                            if(InitSequence[i].name=="RainInit")
                            {
                                InitSequence[i].SetActive(true);
                            }
                        }
                        break;
                    case ItemName.GunsOfRetribution:
                        locked=true;
                        //initiating the cooldown
                        InitiateTheCooldown(waitForSeconds);

                        if(count>0)
                        {
                            GetComponent<Image>().color=Color.gray;
                            tempcolor=GetComponent<Image>().color;
                            tempcolor.a=0.8f;
                            GetComponent<Image>().color=tempcolor;
                        }
                        for(int i=0;i<2;i++)
                        {
                            
                            if(otherTwo[i].GetComponent<ItemSlotInGamePlay>().count>0)
                            {
                                otherTwo[i].GetComponent<ItemSlotInGamePlay>().locked=true;
                                otherTwo[i].GetComponent<Image>().color=Color.gray;
                                tempcolor=otherTwo[i].GetComponent<Image>().color;
                                tempcolor.a=0.8f;
                                otherTwo[i].GetComponent<Image>().color=tempcolor;
                            }

                        }

                        for(int i=0;i<InitSequence.Length;i++)
                        {
                            if(InitSequence[i].name=="TurretInit")
                            {
                                InitSequence[i].SetActive(true);
                            }
                        }
                        break;
                    case ItemName.NectarOfTheGods:
                        UseNectarOfTheGods();
                        break;
                    case ItemName.AmmoPack:
                        UseAmmoPack();
                        break;


                }

            }

        }
    }
    public void UseNectarOfTheGods(bool fromAd=false)
    {
        if(fromAd)
        {
            AudioManager.instance.mech_Audio_source.Play();
            AudioManager.instance.itemAudioSource.Play();
        }
        TouchMovement.instance.clutch=false;
        AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.nectar_of_gods,AudioManager.instance.volume);
        if(EnemySpawner.instance.friend)
        {
            for(int i=0;i<EnemySpawner.instance.listOfFriendlyNumbers.Count;i++)
            {
                if(EnemySpawner.instance.listOfFriendlyNumbers[i].transform.localScale.x > 3f)
                {
                    EnemySpawner.instance.listOfFriendlyNumbers[i].GetComponent<MovementOfTheNumbers>().ExecuteTheEffectsOfRemovalOfTheCoverNumber(true);
                    EnemySpawner.instance.listOfFriendlyNumbers[i].GetComponent<NumberBehaviour>().orderChangeBackTheColor();
                    //doing this so that after the destruction of the cover number this if statement is never again called
                    var tempLocalScale=new Vector3(3f,3f,3f);
                    EnemySpawner.instance.listOfFriendlyNumbers[i].transform.localScale=tempLocalScale;
                    //adding the slowMo-
                    EnemySpawner.instance.listOfFriendlyNumbers[i].GetComponent<MovementOfTheNumbers>().ExecuteSloMoOfDestructionOfSLoMO=true;

                    EnemySpawner.instance.listOfFriendlyNumbers[i].GetComponent<IndividualNumberProvider>().AssisgnTheNumber("NA");

                }               
            }

        }
        else
        {
            for(int i=0;i<EnemySpawner.instance.listOfEnemyNumbers.Count;i++)
            {
                EnemySpawner.instance.listOfEnemyNumbers[i].GetComponent<IndividualNumberProvider>().numberOnTheEntity.text=""+0;
                ParticleInitiation.instance.CoverParticleDestruction(EnemySpawner.instance.listOfEnemyNumbers[i].transform.position,Color.red);
            }
        }
        var player=GameObject.Find("Player");
        if(((int)double.Parse(player.GetComponent<PlayerNumberUpdater>().text.text))<0)
        {
            player.GetComponent<PlayerNumberUpdater>().text.text=""+0;
        }
        player.GetComponent<PlayerInvinci>().MakePlayerInvincible();
        circleMovement.instance.cantBeControlled=false;

        ParticleInitiation.instance.CoverParticleDestruction(player.transform.position,player.GetComponent<SpriteRenderer>().color);

    }
    public void UseAmmoPack()
    {
        AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.ammo_pack,AudioManager.instance.volume);
        var powerupAmmo=Instantiate(ammo_prefab,transform.position,Quaternion.identity);
        powerupAmmo.GetComponent<CoinPowerUpPrefab>().AssignTheAmmoValuesToThePowerUp(1);
        powerupAmmo.GetComponent<CoinPowerUpPrefab>().taken=true;

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InitiateTheCooldown(float time)
    {
        StartCoroutine(StartTheLoad(time));
    }
    IEnumerator StartTheLoad(float time)
    {
        yield return new WaitForSeconds(time);
        
        if(!loading&&count>0)
        {
            locked=false;
            var t1=GetComponent<Image>().color;
            t1=Color.white;
            t1.a=1f;
            GetComponent<Image>().color=t1;
        }
        for(int i=0;i<2;i++)
        {
            if(otherTwo[i].GetComponent<ItemSlotInGamePlay>().count>0&&!otherTwo[i].GetComponent<ItemSlotInGamePlay>().loading)
            {
                otherTwo[i].GetComponent<ItemSlotInGamePlay>().locked=false;

                otherTwo[i].GetComponent<Image>().color=Color.white;
                var tempcolor=otherTwo[i].GetComponent<Image>().color;
                tempcolor.a=1f;
                otherTwo[i].GetComponent<Image>().color=tempcolor;
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        if(loading==true)
        {
            temp_countDown-=Time.deltaTime;
            textOnItem.text=""+temp_countDown.ToString("0");

            if(temp_countDown<=0f)
            {
                loading=false;
                BulletInstantiater.instance.bulletMultiplier=BulletMultiplier.X1;
                if(count>0)
                {
                    locked=false;
                    var t1=GetComponent<Image>().color;
                    t1=Color.white;
                    t1.a=1f;
                    GetComponent<Image>().color=t1;
                    textOnItem.text="";
                }    
                else
                {
                    var t1=GetComponent<Image>().color;
                    t1=Color.white;
                    t1.a=0f;
                    GetComponent<Image>().color=t1;
                    textOnItem.text="";

                }
            }
        }
    }
}
