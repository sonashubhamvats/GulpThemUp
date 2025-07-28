using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SceneCounter : MonoBehaviour
{
    public TextMeshProUGUI counter_text;

    public static SceneCounter instance;

    public Image border_colors;

    [HideInInspector]public float interval;
    
    public bool stopBlinking;
    private void Awake()
    {
        MakeInstance();
        counter_text.text="0/"+GameManager.instance.curr_scene_counter;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void UpdateTheCounter()
    {
        string[] temp_str_curr_count=counter_text.text.Split('/');
        int count=(int)double.Parse(temp_str_curr_count[0]);
        count++;
        counter_text.text=count+"/"+GameManager.instance.curr_scene_counter;
    }
    // Update is called once per frame
    void Update()
    {
        if(!stopBlinking)
        {
            if(interval>=0.3f)
            {
                var temp_text_color=counter_text.color;
                var temp_border_color=border_colors.color;
                interval=0f;
                if(temp_text_color.a>0f)
                {
                    temp_border_color.a=0f;
                    temp_text_color.a=0f;
                }
                else
                {
                    temp_border_color.a=1f;
                    temp_text_color.a=1f;
                }
                counter_text.color=temp_text_color;
                border_colors.color=temp_border_color;
            }
            interval+=Time.deltaTime;

        }
    }
}
