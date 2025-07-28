using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputSettingsManager : MonoBehaviour
{

    [Range(0.1f, 1f)]
    public float JoyStickSensitivity;
    [Range(0.1f, 1f)]
    public float TouchSensitivity;

    public float volume;
    public static InputSettingsManager instance;
    public void Awake()
    {
        MakeInstance();

    }
    void MakeInstance()
    {

        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        JoyStickSensitivity=GameManager.instance.joystickSens;
        TouchSensitivity=GameManager.instance.touchSens;
        volume=GameManager.instance.volume;

    }
}
