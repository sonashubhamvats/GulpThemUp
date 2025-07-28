using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public Slider joyStick,touch,sound;
    public TextMeshProUGUI pause_level_text,pause_obj_1,pause_obj_2,pause_obj_3;
    public static ButtonManager instance;

    public Color selectedColor,white;
    public Button Input1,Input2;
    public GameObject pausePanel,forfeit_panel,watch_ad_panel;
    public GameObject startPanel;

    public GameObject credits_panel,loadingScene;

    public Image progressBar;
    private BulletCountProvider bulletCountProvider;
    public GameObject tutorialPromptStart;
    public GameObject tutorialPoint;
    
    //declare matrix and calibration vector
    public void Awake()
    {
        MakeInstance();
        Time.timeScale = 1f;
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
 
    //menu functions
    
    public void DisablePromptTutorial()
    {
        tutorialPoint.SetActive(false);
        tutorialPromptStart.SetActive(false);
    }
    public void BackToTheMainMenu()
    {
        InSceneManagerInMainMenu.instance.BackToTheMainMenu();
    }
    public void BackToTheMainMenuFromSettings()
    {
        
        GameManager.instance.joystickSens=joyStick.value;
        GameManager.instance.touchSens=touch.value;
        GameManager.instance.volume=sound.value;
        InputSettingsManager.instance.JoyStickSensitivity=joyStick.value;
        InputSettingsManager.instance.volume=sound.value;
        InputSettingsManager.instance.TouchSensitivity=touch.value;
        AudioManager.instance.AdjustGameVolume();
        InSceneManagerInMainMenu.instance.BackToTheMainMenu();
    }

    public void callSettings()
    {
        InSceneManagerInMainMenu.instance.callSettings();
    }
    public void callLevelSelector()
    {
        InSceneManagerInMainMenu.instance.callLevelSelector();
    }
    public void JoyStickInput2()
    {
        GameManager.instance.InputType = 1;
        if(Input2.GetComponent<Image>().color!=selectedColor)
        {
            Input2.GetComponent<Image>().color=selectedColor;
            Input1.GetComponent<Image>().color=white;
        }
            
    }
    public void JoyStickInput1()
    {
        GameManager.instance.InputType = 0;
        if(Input1.GetComponent<Image>().color!=selectedColor)
        {
            Input1.GetComponent<Image>().color=selectedColor;
            Input2.GetComponent<Image>().color=white;
        }

    }
    public void Credits()
    {
        credits_panel.SetActive(true);
        AudioManager.instance.ButtonClickSound();
    }
    public void StartGameWithLoadingScreen()
    {
        loadingScene.SetActive(true);
        StartCoroutine(LoadSceneAsync("GamePlay"));

    }
    public void Zapsplat()
    {
        Debug.Log("Click");
        Application.OpenURL("https://www.zapsplat.com/");
    }
    public void AboutGame()
    {
        AudioManager.instance.ButtonClickSound();
    }
    public void StartConfirm()
    {
        AudioManager.instance.ButtonClickSound();
        GameManager.instance.InitiateStartGame();
    }
    public void CancelStartConfirm()
    {
        AudioManager.instance.ErrorSound();
        startPanel.gameObject.SetActive(false);
    }
    public void OpenStartPanel()
    {
        AudioManager.instance.SelectOptionSound();
        startPanel.gameObject.SetActive(true);
    }

    public void StartTheTutorial()
    {
        ObjectiveManager.instance.UpdateTheObjectives(4);
        GameManager.instance.stalled_level_no=4;
        GameManager.instance.InTutorial=true;
        AudioManager.instance.ButtonClickSound();
        GameManager.instance.InitiateStartGame();

        

    }
    //LevelSelectionButton
 

    //menu functions end


    //gamerplay functions

    public void forfeitAccepted()
    {
        watch_ad_panel.SetActive(false);
        forfeit_panel.SetActive(true);
    }
    public void tryAgain()
    {
        loadingScene.SetActive(true);
        StartCoroutine(LoadSceneAsync("GamePlay"));
    }
    public void resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        //disabling the controls for the player
        CrossHairMovement.instance.cantBeControlled=false;
        circleMovement.instance.cantBeControlled=false;
        CannonCharacter.instance.cantBeControlled=false;
        TouchMovement.instance.clutch=false;
        AudioManager.instance.mech_Audio_source.Play();
        AudioManager.instance.itemAudioSource.Play();

    }
    public void PauseButton()
    {
        //disabling any other touch disruptions
        TouchMovement.instance.clutch=true;
        pause_level_text.text="Level-"+GameManager.instance.levelno;
        pause_obj_1.text=GameManager.instance.objPoints[0];
        pause_obj_2.text=GameManager.instance.objPoints[1];
        pause_obj_3.text=GameManager.instance.objPoints[2];
        pausePanel.SetActive(true);
        //disabling the controls for the player
        CrossHairMovement.instance.cantBeControlled=true;
        circleMovement.instance.cantBeControlled=true;
        CannonCharacter.instance.cantBeControlled=true;
        Time.timeScale = 0f;
        AudioManager.instance.mech_Audio_source.Pause();
        AudioManager.instance.itemAudioSource.Pause();




    }
    public void back()
    {
       
        //clearing the list of selected items once the gamplay ends
        GameManager.instance.listOfSelectedItems.Clear();
        loadingScene.SetActive(true);
        GameManager.instance.InTutorial=false;
        StartCoroutine(LoadSceneAsync("MainMenu"));
       
    }
    public void BackFromForfietAndComplete()
    {
        //clearing the list of selected items once the gamplay ends
        GameManager.instance.listOfSelectedItems.Clear();
        //adding the level counter
        GameManager.instance.levelCounter_ad++;
        loadingScene.SetActive(true);
        StartCoroutine(LoadSceneAsync("MainMenu"));

    }
    public IEnumerator LoadSceneAsync(string LeveName)
    {
        
        AsyncOperation operation=SceneManager.LoadSceneAsync(LeveName);
        
        while(!operation.isDone)
        {
            progressBar.fillAmount=operation.progress;
            yield return null;
        }
    }

    //gameplay functions end
}
