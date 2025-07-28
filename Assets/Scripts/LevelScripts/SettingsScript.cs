using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsScript : MonoBehaviour
{
    public Slider volume,touch,joystick;
    // Start is called before the first frame update
    private void OnEnable()
    {
        volume.onValueChanged.AddListener(delegate{ChangeVolume();});
        volume.value=InputSettingsManager.instance.volume;
        touch.value=InputSettingsManager.instance.TouchSensitivity;
        joystick.value=InputSettingsManager.instance.JoyStickSensitivity;
    }
    void ChangeVolume()
    {
        AudioManager.instance.audioSources[0].volume=volume.value;
    }
}
