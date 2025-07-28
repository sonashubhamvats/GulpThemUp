using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BulletChange : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public Sprite[] sprite_prefabs;

    public int unlocked_Indexes,current_index;

    public List<Sprite> Ingame_sprites_bullet=new List<Sprite>();
    public void OnPointerDown(PointerEventData eventData)
    {
        current_index++;
        if(current_index>unlocked_Indexes)
        {
            current_index=0;
        }
        GetComponent<Image>().sprite=Ingame_sprites_bullet[current_index];
        //calling upadating the ammo sprite here



        //bullet firing for the first time so there should be no wait time
        PlayerAttack.instance.firstBullet=true; 
        
        //updating the bullet type
        UpdateBulletType(current_index);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       
    }

    private void Awake() {

        //ensuring that some types of bullets are only available at some levels
        Debug.Log(GameManager.instance.types_of_bullets);
        unlocked_Indexes=GameManager.instance.types_of_bullets-1;
        for(int i=0;i<=unlocked_Indexes;i++)
        {
            Ingame_sprites_bullet.Add(sprite_prefabs[i]);
        }
        current_index=0;
    }
    void UpdateBulletType(int index)
    {
        switch(index)
        {
            case 0:
                BulletInstantiater.instance.curr_bullet_type=BulletType.normal;
                //calling Updating the bullet count here
                InGameStatManager.instance.ChangeTheBUlletCount(InGameStatManager.instance.normalCOunt.ToString());
                break;
            case 1:
                BulletInstantiater.instance.curr_bullet_type=BulletType.piercing;
                //calling Updating the bullet count here
                InGameStatManager.instance.ChangeTheBUlletCount(InGameStatManager.instance.PierceCount.ToString());
                break;
            case 2:
                BulletInstantiater.instance.curr_bullet_type=BulletType.reflective;
                //calling Updating the bullet count here
                InGameStatManager.instance.ChangeTheBUlletCount(InGameStatManager.instance.ReflectiveCount.ToString());
                break;
            case 3:
                BulletInstantiater.instance.curr_bullet_type=BulletType.missile;
                //calling Updating the bullet count here
                InGameStatManager.instance.ChangeTheBUlletCount(InGameStatManager.instance.MissileCount.ToString());
                break;
        }
        PlayerAttack.instance.UpdateTheFirerate();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
