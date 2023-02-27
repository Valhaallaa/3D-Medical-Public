using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PatientHead : MonoBehaviour
{

    private PatientSystem _Patient;
    private bool _AirwayClearencePerformed, _OxygenMaskOn;
    [SerializeField]
    Canvas HeadWheel;
    PlayerController _PlayerController;
    ToolsBase _ToolUsed;

    public void CheckAirway()
    {
        switch (_Patient._ObstructionLevel)
        {
            case 0: // No Obstruction
                ClearAirwayFeeback();
                break;

            case 1:// Partial Obstruction
                PartialBlockedAirwayFeedback();
                break;

            case 2: // Full Obstruction
                BlockedAirwayFeedback();
                break;
                
            default:
                ClearAirwayFeeback();
                break;

        }
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckAirway);
    }

    private void ClearAirwayFeeback()
    {
        FeedbackHandler._Handler.SetText("Airway Unobstructed", "The airway is clear");
        Debug.Log("No Airway Obstruction");
    }

    private void BlockedAirwayFeedback()
    {
        FeedbackHandler._Handler.SetText("Airway Obstructed", "The airway is Obstructed");
        Debug.Log("Full Airway Obstruction");
    }

    private void PartialBlockedAirwayFeedback()
    {
        FeedbackHandler._Handler.SetText("Airway Partially Obstructed", "The airway is Partially Obstructed");
        Debug.Log("Partial Airway Obstruction");
    }


    public void AirwayClearance()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<Yank>())
        {
            FeedbackHandler._Handler.SetText("Airway Clearance", "The Airway has been cleared.");
            Debug.Log("Airway Clearance");

            _AirwayClearencePerformed = true;
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._ClearAirway);
            if (_Patient._ObstructionLevel > 1)
                _Patient._ObstructionLevel = 0;
        } 
   
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect Tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();

        

    }

    public void AirwayIntubation()
    {
        if(_ToolUsed && _ToolUsed.GetComponent<AirwayTubeTool>())
        {
            FeedbackHandler._Handler.SetText("Airway Intubation Done", "Airway Intubation Done");
            Debug.Log("Airway Intubation Done");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._ClearAirway);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect Tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();


    }

    public void ApplyOxygenMask()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<OxygenMaskTool>())
        {
            FeedbackHandler._Handler.SetText("Oxygen Mask Applied", "Oxygen Mask Applied");
            Debug.Log("Oxygen Mask Applied");
            _OxygenMaskOn = true;
            int _CurrentOxygenSaturation = _Patient._OxygenSaturation;
            int _NewOxygenSaturation = _CurrentOxygenSaturation += 12; //Replace 12 with the amount on the tool class when it gets created
            _Patient._OxygenSaturation = _NewOxygenSaturation;
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._ApplyOxygen);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect Tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();


    }

    public void TakeTemperature()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<ThermometerTool>())
        {
            FeedbackHandler._Handler.SetText("Temerature Taken", "Temp: " + _Patient._Temperature + "°C");
            Debug.Log("take temperature");
            Debug.Log("Temp: " + _Patient._Temperature + "°C");

            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._TakeTemp);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect Tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();


    }

    public void CheckPupils()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<TorchTool>())
        {
            FeedbackHandler._Handler.SetText("Checked Pupils", "Pupil Size: " + _Patient._PupilSize);
            Debug.Log("check pupils");
            Debug.Log("Pupil Size: " + _Patient._PupilSize);
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckPupils);
        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect Tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();

        

    }

    public void CheckSeizing()
    {
        if (_Patient._IsSeizing)
        {
            // is seizing
            FeedbackHandler._Handler.SetText("Seizing", "Patient is Seizing");
            Debug.Log("is seizing");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckSeizing);

        }
        if (!_Patient._IsSeizing)
        {
            // not seizing
            FeedbackHandler._Handler.SetText("Not Seizing", "Patient is Not Seizing");
            Debug.Log("is NOT seizing");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckSeizing);

        }
        CloseInteractionWheel();


    }

    public void CheckConsciousness()
    {
        FeedbackHandler._Handler.SetText("Is the Patient Concious", _Patient._IsConscious.ToString());
        Debug.Log("Consciousness: " + _Patient._IsConscious);
        CloseInteractionWheel();

        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckConsciousness);

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
            HeadWheel.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }

    public void CloseInteractionWheel()
    {
        //locks the cursor and unlocks the players movement
        DataManager._Data._WheelOpen = false;
        HeadWheel.gameObject.SetActive(false);
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
