using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
[System.Serializable]
public struct colors
{
    public Color[] colours;
}

public class GameColorUnification : MonoBehaviour
{
    public SpriteRenderer Background;

    public TextMeshProUGUI sceneCounter;

    public Image counter_bounder;
    public colors listOfcolors;

    public Color negativeColor;
    public Color selectedColor;
    public static GameColorUnification instance;

    public SpriteRenderer player,cannon,crosshair;
    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        
        selectedColor=listOfcolors.colours[Random.Range(0,10)];
        player.color=selectedColor;
        ChangeTheColorOfTheNonColliderObjects();
    }
    public void ChangeTheColorOfTheObject(GameObject obj)
    {
        var sprite=obj.GetComponent<SpriteRenderer>();
        if(sprite!=null)
        {
            sprite.color=selectedColor;
        }
    }
    public void ChangeTheColorOfTheBullets()
    {
        BulletInstantiater.instance.StartColorOfTheBullet=selectedColor;
    }
    public void ChangeTheSelectedColor(bool friend)
    {
        if(friend==true)
        {
            selectedColor=listOfcolors.colours[Random.Range(0,10)];
        }
        else
        {
            selectedColor=negativeColor;
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
    // Update is called once per frame
    public void ChangeTheColorOfTheNonColliderObjects()
    {
        Background.color=selectedColor;
        sceneCounter.color=selectedColor;
        counter_bounder.color=selectedColor;
       
    }
    void Update()
    {
        
    }
}
