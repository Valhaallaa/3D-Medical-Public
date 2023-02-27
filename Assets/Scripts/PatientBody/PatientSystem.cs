using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSystem : MonoBehaviour
{

    public static PatientSystem _PatientInfo;
    // Diagnose Information
    private bool _IssueFound = false;

    // overall Information
    public bool _CanTalk;
    public bool _Responsive;
    public int _HeartBeatsPM;
    public bool _IsSeizing, _HasIV, _BloodTaken;
    public string _IsConscious;
    public int _OxygenSaturation, _RespitoryRate, _CapillaryRefillTime, _PupilSize;
    public float _Temperature, _GlucoseLevel;
    public string _SkinColourDescription, _DepthOfRespiration, _ChestSymmetry, _AccessoryMusclesUsed, _PainDescription, _ChestNoiseDescription, _RespRateDescription, _AssessmentDescription,_BloodPressure, _UrineOutputDescription ;

    //Airway Information
    public int _ObstructionLevel;

    //Breathing Information
    //public int _BreathingLevel;



    public void FindIssue()
    {
        if(_IssueFound != true)
            _IssueFound = true;
    }

    public bool CheckIssue()
    {
        return _IssueFound;
    }

    private void Start()
    {
        if (_PatientInfo == null)
            _PatientInfo = this;
        else
            Destroy(gameObject);
    }
}
