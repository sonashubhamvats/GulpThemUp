using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameStatManager : MonoBehaviour
{
    public TextMeshProUGUI CoinCount,BulletCount;
    public int normalCOunt,PierceCount,ReflectiveCount,MissileCount;
    public static InGameStatManager instance;

    public GameObject[] itemSlots;
    // Start is called before the first frame update
    private void Awake() {
        MakeInstance();
        AssignTheItemsToItsSlots();
    }
    void AssignTheItemsToItsSlots()
    {
        for(int i=0;i<GameManager.instance.listOfSelectedItems.Count;i++)
        {
            itemSlots[i].GetComponent<ItemSlotInGamePlay>().UpdateTheValuesOfTheSlot(GameManager.instance.listOfSelectedItems[i].name,
            GameManager.instance.listOfSelectedItems[i].quantity,GameManager.instance.listOfSelectedItems[i].icon);
        }
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
        MissileCount=GameManager.instance.MissileCount;
        PierceCount=GameManager.instance.PiercingCount;
        ReflectiveCount=GameManager.instance.reflectiveCount;
        normalCOunt=GameManager.instance.normalCount;

        CoinCount.text="0";
        //here some changes are to be made where we need more ammo at the start and more ammo pickup
        
        BulletCount.text=normalCOunt.ToString();
    }

    //called from the player attack functuion to decrease the total no of bullets
    public void decreaseTheBulletNo(BulletType type,int lossOfBUllets=0)
    {
        int count=(int)double.Parse(BulletCount.text);
        switch (type)
        {
            case BulletType.normal:
                normalCOunt--;
                normalCOunt-=lossOfBUllets;
                break;
            case BulletType.piercing:
                PierceCount--;
                normalCOunt-=lossOfBUllets;
                break;
            case BulletType.reflective:
                ReflectiveCount--;
                break;
            case BulletType.missile:
                MissileCount--;
                break;
        }
        count-=(lossOfBUllets+1);
        if(count>=0)
        {
            BulletCount.text=count.ToString();
        }
    }
    public void IncreaseTheBulletCount(int na,int pa,int ra,int ma)
    {
        //increasing the bullet count
        normalCOunt+=na;
        PierceCount+=pa;
        ReflectiveCount+=ra;
        MissileCount+=ma;
        
        int count_temp=(int)double.Parse(BulletCount.text);
        
        switch (BulletInstantiater.instance.curr_bullet_type)
        {
            case BulletType.normal:
                count_temp=normalCOunt;
                break;
            case BulletType.piercing:
                count_temp=PierceCount;
                break;
            case BulletType.reflective:
                count_temp=ReflectiveCount;
                break;
            case BulletType.missile:
                count_temp=MissileCount;
                break;
        }
        BulletCount.text=count_temp.ToString();
        
    }
    public void IncreaseTheCoinCount()
    {
        int coin_count=(int)double.Parse(CoinCount.text);
        coin_count++;
        GameManager.instance.CoinCount++;
        CoinCount.text=coin_count.ToString();
    }
    //returning the bullet count used to check of the total no of bullets is > 0 or not in the playerattack
    public int returnBulletCount()
    {
        int count=(int)double.Parse(BulletCount.text);
        return count;
    }
    //assiging the bulletCount
    public void ChangeTheBUlletCount(string text)
    {
        BulletCount.text=text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
