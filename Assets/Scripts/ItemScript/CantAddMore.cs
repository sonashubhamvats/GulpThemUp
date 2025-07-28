using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CantAddMore : MonoBehaviour
{
    public float speedOfDisap;
    public TextMeshProUGUI cantAddText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("called");
        var temp_color=GetComponent<Image>().color;
        temp_color.a=1f;
        GetComponent<Image>().color=temp_color;

        temp_color=cantAddText.color;
        temp_color.a=1f;
        cantAddText.color=temp_color;
    }
    // Update is called once per frame
    void Update()
    {
        var temp_color=GetComponent<Image>().color;
        temp_color.a-=Time.deltaTime*speedOfDisap;
        GetComponent<Image>().color=temp_color;

        temp_color=cantAddText.color;
        temp_color.a-=Time.deltaTime*speedOfDisap;
        cantAddText.color=temp_color;

        if(temp_color.a<=0f)
        {
            gameObject.SetActive(false);
        }
       
    }
}
