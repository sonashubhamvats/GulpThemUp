using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBehaviour : MonoBehaviour
{
    private TrailRenderer trail;
    private SpriteRenderer sprite;
    private Color32[] color = {new Color32(139,25,231,255),new Color32(227,231,25,255)
                            ,new Color32(231,52,25,255),new Color32(25,97,231,255)};
    // Start is called before the first frame update
    public GameObject effect;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        //setting the trail's width 
        trail = GetComponent<TrailRenderer>();
        trail.startWidth = (transform.localScale.x - 2f) * 0.6f;
        if (gameObject.tag == "Enemy")
        {
            trail.startWidth = (transform.localScale.x - 2f) * 0.2f;
            effect.SetActive(true);
        }
        trail.endWidth = 0f;
        if (gameObject.transform.localScale.x == 5f)
        {
            if (gameObject.tag == "Friend")
            {
                trail.time = 1.5f;
                trail.startWidth = 2f;
            }
        }//setting the trail's width and time

    }
    public void orderColorChange()
    {
        float alpha = 1f;
        int index = Random.Range(0, 4);
        
        if (gameObject.transform.localScale.x > 3f)
        {

            sprite.color = color[index];
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(color[index], 0.78f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(0f, 1.0f) }
            );
            trail.colorGradient = gradient;
        }
        else
        {
            effect.SetActive(true);
        }
        
    }
    public void orderChangeBackTheColor()
    {
        effect.SetActive(true);
        float alpha = 1f;
        sprite.color = Color.green;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.78f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(0f, 1.0f) }
        );
        trail.colorGradient = gradient;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
