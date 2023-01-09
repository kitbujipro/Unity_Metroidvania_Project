using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   
    public GameObject helpPanel;
    public GameObject settingsGroup;
    public GameObject canvas;

    Animator canvasAnim;

    public Button playButton;
    public Button settingsButton;
    public Button helpButton;
    public Button creditsButton;
    public Button quitButton;
    public Button backButton;
    public Button backButtonHelp;
    public Button backButtonSettings;
    public Button resumeButton;
    static bool isPlayClicked = false;
    public GameObject platTV;
    public GameObject arpgTV;

    CameraScript cameraScript;
    GameMaster gm;
    PlayerMovement playerMovement;
    [SerializeField] Slider creditsSlider;
    public bool isAnimFinished = false;

    [SerializeField] CanvasGroup settingsGroupAlpha;
    [SerializeField] CanvasGroup helpGroupAlpha;

    float helpCounter;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        if (gm.lastCheckPointPos == new Vector3(14, 7, 20)) //10 is the magic number
        {
            Invoke("SwitchToCredits", 0.5f);
            //SwitchToCredits();
        }

        cameraScript = GameObject.FindObjectOfType<CameraScript>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        canvasAnim = canvas.GetComponent<Animator>();

        StartofGame();
    }

    public void StartofGame()
    {
        //main buttons
        playButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        helpButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        resumeButton.gameObject.SetActive(false);
        //back button
        backButton.gameObject.SetActive(false);

        //images & groups
        helpPanel.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);

        creditsSlider.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SwitchToMenu()
    {
        cameraScript.SwitchToMenuCam();
        FindObjectOfType<AudioManager>().Play("ButtonClick");


        //main buttons
        /*
        playButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        helpButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        */
        canvasAnim.Play("StartMainButtons");

        //back button
        backButton.gameObject.SetActive(false);

        //images & groups
        helpPanel.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        creditsSlider.gameObject.SetActive(false);
        backButtonHelp.gameObject.SetActive(false);
        backButtonSettings.gameObject.SetActive(false);
        settingsGroupAlpha.alpha = 0;
        helpGroupAlpha.alpha = 0;
    }

    public void SwitchToPlay()
    {
        cameraScript.SwitchToPlayCam();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        canvasAnim.Play("EndMainButtons");
        isPlayClicked = true;
    }

    public void SwitchToHelp()
    {
        cameraScript.SwitchToHelpCam();
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        //main buttons
        /*
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        helpButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        */

        canvasAnim.Play("EndMainButtons");

        StartCoroutine(HelpPanel());
    }

    public void SwitchToSettings()
    {
        cameraScript.SwitchToSettingsCam();
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        //main buttons
        /*
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        helpButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        */

        canvasAnim.Play("EndMainButtons");
       


        //images & groups
        StartCoroutine(EnableSettingsGroup());
    
    }

    public void SwitchToCredits()
    {
        cameraScript.SwitchToCreditsCam();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        backButtonHelp.gameObject.SetActive(false);
        backButtonSettings.gameObject.SetActive(false);
        //main buttons
        /*
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        helpButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        */

        canvasAnim.Play("EndMainButtons");

        //back button
        backButton.gameObject.SetActive(true);

        if(isAnimFinished == true)
        {
            creditsSlider.gameObject.SetActive(true);
        }
        
    }

    public void OnClickHelpPanel()
    {
        /*
        switch(helpCounter)
        {
            case 1:
                platTV1.SetActive(true);
                platTV2.SetActive(false);
                arpgTV1.SetActive(false);
                arpgTV2.SetActive(false);
                helpCounter++;
                break;
            case 2:
                platTV1.SetActive(false);
                platTV2.SetActive(true);
                arpgTV1.SetActive(false);
                arpgTV2.SetActive(false);
                helpCounter++;
                break;
            case 3:
                platTV1.SetActive(false);
                platTV2.SetActive(false);
                arpgTV1.SetActive(true);
                arpgTV2.SetActive(false);
                helpCounter++;
                break;
            case 4:
                platTV1.SetActive(false);
                platTV2.SetActive(false);
                arpgTV1.SetActive(false);
                arpgTV2.SetActive(true);
                helpCounter = 1;
                break;
        }
        */
        
        if(platTV.activeSelf == true)
        {
            platTV.SetActive(false);
            arpgTV.SetActive(true);
        }
        else
        {
            platTV.SetActive(true);
            arpgTV.SetActive(false);
        }
        
    }

    IEnumerator EnableSettingsGroup()
    {
        settingsGroup.gameObject.SetActive(true);
        backButtonSettings.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
      //  canvasAnim.Play("SettingsAnimation");
       
       
      
        while (settingsGroupAlpha.alpha < 1)
        {
            settingsGroupAlpha.alpha += .7f * Time.deltaTime; ;
            yield return null;
        }
        yield return null;

      
       


    }

    IEnumerator HelpPanel()
    {
        helpPanel.SetActive(true);
        backButtonHelp.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        
        while (helpGroupAlpha.alpha < 1)
        {
            helpGroupAlpha.alpha += .5f * Time.deltaTime; ;
            yield return null;
        }
        yield return null;

    }

    void Update()
    {
        if (isPlayClicked == true && gm.gameOver != true)
        {
            resumeButton.gameObject.SetActive(true);

        }
    }


}
