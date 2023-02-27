using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class PatientArm : MonoBehaviour
{
    private PatientSystem _Patient;
    [SerializeField]
    Canvas _ArmWheel;
    public PlayerController _PlayerController;
    ToolsBase _ToolUsed;

    public void MeasurePulse()
    {
        FeedbackHandler._Handler.SetText("Patient Pulse", "The patients pulse is: " + _Patient._HeartBeatsPM);
        Debug.Log("The patients pulse is: " + _Patient._HeartBeatsPM);

        CloseInteractionWheel();
        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._MeasurePulse);
    }

    public void MeasureBloodPressure()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<BloodPressureTool>())
        {
            FeedbackHandler._Handler.SetText("Patients BP", "The patients BP is: " + _Patient._BloodPressure);
            Debug.Log("The patients BP is: " + _Patient._BloodPressure + " BPM");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._MeasureBloodPressure);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect tool");

            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();
    }

    public void MeasureCapillaryRefillTime()
    {
        Debug.Log("The capillary refill time was " + _Patient._CapillaryRefillTime + " seconds");
        FeedbackHandler._Handler.SetText("Capillary Refil Time", "The capillary refill time was " + _Patient._CapillaryRefillTime + " seconds");
        CloseInteractionWheel();
        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._MeasureCapillaryRefill);
    }

    public void InsertIV()
    {
        if (_Patient._HasIV)
        {
            ApplyIVFluids();
        }

        if (!_Patient._HasIV)
        {
            if (_ToolUsed && _ToolUsed.GetComponent<IVTool>())
            {
                FeedbackHandler._Handler.SetText("IV Inserted", "IV Inserted");
                Debug.Log("IV Inserted");
                _Patient._HasIV = true;
                ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InsertIV);
                GameObject.FindGameObjectWithTag("IVText").GetComponent<TextMeshProUGUI>().text = "Apply IV Fluids";
            }
            else
            {
                FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
                Debug.Log("Incorrect tool");
                ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

            }
        }


        CloseInteractionWheel();
    }

    public void MeasureGlucoseLevel()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<GlucoseTool>())
        {
            FeedbackHandler._Handler.SetText("Glucose Level", "The patients glucose level is: " + _Patient._GlucoseLevel + " mmols");
            Debug.Log("The patients glucose level is: " + _Patient._GlucoseLevel);
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._MeasureGlucoseLevel);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();

    }

    public void TakeBloods()
    {
        if (_ToolUsed && _ToolUsed.GetComponent<BloodTubesTool>())
        {
            Debug.Log("Blood taken ");
            FeedbackHandler._Handler.SetText("Blood Taken", "Patients Blood Taken");
            _Patient._BloodTaken = true;
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._TakeBloods);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();

    }


    public void CheckOxygenSaturation()
    {
        // return OxSaturation Value
        if (_ToolUsed && _ToolUsed.GetComponent<PulseOximeter>())
        {
            FeedbackHandler._Handler.SetText("Oxygen Saturation", "Oxygen Saturation: " + _Patient._OxygenSaturation);
            Debug.Log("Ox Saturation: " + _Patient._OxygenSaturation);
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._CheckOxygenSaturation);

        }
        else
        {
            FeedbackHandler._Handler.SetText("Incorrect Tool", "Wrong Tool");
            Debug.Log("Incorrect Tool");
            ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._InvalidTool);

        }
        CloseInteractionWheel();

    }

    public void ApplyIVFluids()
    {

        FeedbackHandler._Handler.SetText("Fluids Applied", "Fluids applid to Patient");
        ScoringSystem._Instance.TaskChecker(ScoringSystem.Task._ApplyFluids);
        CloseInteractionWheel();

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
            _ArmWheel.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }

    public void CloseInteractionWheel()
    {
        //locks the cursor and unlocks the players movement
        DataManager._Data._WheelOpen = false;
        _ArmWheel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _PlayerController._MovementLocked = false;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    void Start()
    {
        _Patient = PatientSystem._PatientInfo;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Patient == null)
            _Patient = PatientSystem._PatientInfo;
        if (_PlayerController == null)
            _PlayerController = GameObject.FindGameObjectWithTag("PlayerCont").GetComponent<PlayerController>();
    }
}
