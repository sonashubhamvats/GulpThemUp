using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCharacter : MonoBehaviour
{

    public GameObject Player;
    public GameObject CrossHair;

    public bool cantBeControlled;

    public static CannonCharacter instance;
    private void Awake()
    {
        //assiging in the cannon its material and the sprite
        GetComponent<SpriteRenderer>().sprite=GameManager.instance.cannonSprite;
        GetComponent<SpriteRenderer>().material=GameManager.instance.cannonMaterial;

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
    // Update is called once per frame
    void Update()
    {
        if(!cantBeControlled)
        {
            RotateIf();
        }
    }
    void Start()
    {
        //color unification
        GetComponent<SpriteRenderer>().color = Player.GetComponent<SpriteRenderer>().color;
        //changing the bullet color in the beginning
        BulletInstantiater.instance.StartColorOfTheBullet=GetComponent<SpriteRenderer>().color;

    }
    void RotateIf()
    {
        //pointing at the crosshair byu the cannon
        Vector3 diff = CrossHair.transform.position - transform.parent.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.parent.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
