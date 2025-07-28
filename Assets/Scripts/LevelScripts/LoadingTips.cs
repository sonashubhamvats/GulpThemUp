using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadingTips : MonoBehaviour
{
    public TextMeshProUGUI tips;

    public string[] tipsString;
    // Start is called before the first frame update
    private void OnEnable()
    {
        tips.text=tipsString[Random.Range(0,tipsString.Length)];
    }
}
