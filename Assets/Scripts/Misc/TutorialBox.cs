using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class TutorialBox : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Title, Description, Helper;
    [SerializeField]
    GameObject Background;
    PlayerController _PlayerController;
    public static TutorialBox _Tutorial;


    public void SetText(string TitleText, string DescriptionText)
    {

        Title.text = TitleText;
        Description.text = DescriptionText;
        Background.SetActive(true);
        _PlayerController._MovementLocked = true;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = false;
        Time.timeScale = 0;
        if (DataManager._Data != null)
            DataManager._Data._WheelOpen = true;
    }

    public void SetHelper(string text) {

        Helper.text = text;
    }
    public void ClearFeedback()
    {

        _PlayerController._MovementLocked = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked ;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = true;
        Time.timeScale = 1;
        if(DataManager._Data != null)
            DataManager._Data._WheelOpen = false;
        Title.text = " ";
        Description.text = " ";
        Background.SetActive(false);
    }

    public void CloseBox()
    {
        ClearFeedback();

    }

    void Awake()
    {
        if (_Tutorial == null)
            _Tutorial = this;
        else
            Destroy(gameObject);
        _PlayerController = GameObject.FindGameObjectWithTag("PlayerCont").GetComponent<PlayerController>();
        ClearFeedback();


        StartCoroutine((StartDelay()));
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(.5f);
        SetText("Welcome To the Tutorial", "Welcome to the tutorial for this simulation, in this tutorial I will be walking you through the Airway and Breathing steps. At any time you may access the pause menu by pressing the Esc key to see the control." +
            "To begin you want to approach the Dummy on the bed and click on his head and 'Check Airway' Your current task will be in the upper right corner of the screen");
        SetHelper("Check Airway");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
