using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputSettings : MonoBehaviour
{
    public Color selectedColor,white;

    public int InputType;

   public Button otherButton;
    // Start is called before the first frame update
    private void OnEnable() {
        if(InputType==GameManager.instance.InputType)
        {
            GetComponent<Image>().color=selectedColor;
            otherButton.GetComponent<Image>().color=white;
        }
        else
        {
            GetComponent<Image>().color=white;
            otherButton.GetComponent<Image>().color=selectedColor;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
