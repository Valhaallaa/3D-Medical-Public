using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PatientChest : MonoBehaviour
{
    private PatientSystem _Patient;
    [SerializeField]
    Canvas _ChestWheel;
    PlayerController _PlayerController;
    ToolsBase _ToolUsed;

    public void CheckSkinColour()
    {
       Debug.Log("The patients skin looks " + _Patient._SkinColourDescription);
        FeedbackHandler._Handler.SetText("Patients Skin", "The patients skin looks " + _Patient._SkinColourDescription);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckSkinColour);

    }

    public void CheckAccessoryMuscles()
    {
        Debug.Log("Accessory muscles used: " + _Patient._AccessoryMusclesUsed);
        FeedbackHandler._Handler.SetText("Accessory Muscles", "Accessory Muscles used: " + _Patient._AccessoryMusclesUsed);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckAccessoryMuscles);

    }

    public void CheckChestSymmetry()
    {
        Debug.Log("Chest Symmetry is " + _Patient._ChestSymmetry);
        FeedbackHandler._Handler.SetText("Chest Symmetry", "Chest Symmetry is " + _Patient._ChestSymmetry);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckChestSymmetry);

    }

    public void CheckDepthOfRespiration()
    {
        Debug.Log("Depth of Respiration is " + _Patient._DepthOfRespiration);
        FeedbackHandler._Handler.SetText("Respiration Depth", "Depth of Respiration is " + _Patient._DepthOfRespiration);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckDepthOfRespiration);

    }

    public void AssessPain()
    {
        Debug.Log("The patient is experiencing: " + _Patient._PainDescription);
        FeedbackHandler._Handler.SetText("Patient Pain", "The patient is experiencing: " + _Patient._PainDescription);
        // play audio for different pain levels

        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._AssessPain);

    }

    public void ListenToChest()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<StethoscopeTool>())
        {
            Debug.Log("The chest sounds: " + _Patient._ChestNoiseDescription);
            FeedbackHandler._Handler.SetText("Listening to Chest", "The chest sounds: " + _Patient._ChestNoiseDescription);
            // play audio of chest sounds
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._ListenToChest);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect tool");

            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();


    }

    public void MeasureRespitoryRate()
    {
        int _CurrentRespitoryRate = _Patient._RespitoryRate;
        int _NewRespitoryRate = _CurrentRespitoryRate -= 10; //Replace 10 with non-magic number
        _Patient._RespitoryRate = _NewRespitoryRate;
        Debug.Log("Respitory Rate is " + _Patient._RespitoryRate);
        FeedbackHandler._Handler.SetText("Respitory Rate", "Respitory Rate is " + _Patient._RespitoryRate + " & " + _Patient._RespRateDescription);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._MeasureRespitoryRate);

    }
    public void FullBodyAssessment()
    {
        Debug.Log("The patient has " + _Patient._AssessmentDescription);
        FeedbackHandler._Handler.SetText("Assessment Of Body", "The patient has " + _Patient._AssessmentDescription);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._FullAssessment);

    }

    public void CheckUrineOutput()
    {
        Debug.Log("The patient has " + _Patient._UrineOutputDescription);
        FeedbackHandler._Handler.SetText("Assessment Urine Output", _Patient._UrineOutputDescription);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckUrineOutput);

    }

    public void OpenInteractionWheel(PlayerController playerController, ToolsBase toolUsed)
    {
        //unlocks the cursor and locks the players movement
        if (!DataManager._Data._WheelOpen)
        {
            DataManager._Data._WheelOpen = true;
            _ToolUsed = toolUsed; //sets the tool you are using on the head
            _PlayerController = playerController;
            _PlayerController._MovementLocked = true;
            _ChestWheel.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }

    public void CloseInteractionWheel()
    {
        //locks the cursor and unlocks the players movement
        DataManager._Data._WheelOpen = false;
        _ChestWheel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _PlayerController._MovementLocked = false;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        _Patient = PatientSystem._PatientInfo;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Patient == null)
            _Patient = PatientSystem._PatientInfo;
    }




}
