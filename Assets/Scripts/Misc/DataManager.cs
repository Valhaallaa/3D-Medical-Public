using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager _Data;
    public bool _WheelOpen = false;
    public bool _CalledHelp = false;
    public bool Correct = false;
    public bool _IssueWhenCalled;


    public void CallHelp()
    {
        FeedbackHandler._Handler.SetText("Called for Help", "You've called for help");
        Debug.Log("The Player has Called for help, Has an issue been found: " + PatientSystem._PatientInfo.CheckIssue());
        if (_CalledHelp != true)
            _IssueWhenCalled = PatientSystem._PatientInfo.CheckIssue();
        _CalledHelp = true;
        if (PatientSystem._PatientInfo.CheckIssue())
            Correct = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_Data == null)
            _Data = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
