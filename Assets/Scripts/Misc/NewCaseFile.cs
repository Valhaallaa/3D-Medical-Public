using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NewCaseFile : MonoBehaviour
{
    PatientSystem _Patient;
    public bool _CanTalk,_CanBreathe;
    public bool _Responsive;
    public int _HeartBeatsPM, _BloodPressure;
    public bool _IsConscious, _IsSeizing, _HasIV, _BloodTaken;
    public int _OxygenSaturation, _RespitoryRate, _CapillaryRefillTime, _PupilSize;
    public float _Temperature, _GlucoseLevel;
    public string _SkinColourDescription, _DepthOfRespiration, _ChestSymmetry, _AccessoryMusclesUsed, _PainDescription, _ChestNoiseDescription;

    public TextMeshProUGUI text;
    [SerializeField]
    public TMP_InputField _HeartsBeatsPMInput, _BloodPressureInput, _OxygenSaturationInput, _RespitoryRateInput, _CapillaryRefillTimeInput, _PupilSizeInput;
    [SerializeField]
    public TMP_InputField _TemperatureInput, _GlucoseLevelInput, _SkinColourInput, _DepthOfRespirationInput, _ChestSymmetryInput, _AccessoryMusclesInput, _PainInput, _ChestNoiseInput;
    
    public void UpdateValues()
    {

        _HeartBeatsPM = int.Parse(_HeartsBeatsPMInput.text);
        _BloodPressure = int.Parse(_BloodPressureInput.text);
        _OxygenSaturation = int.Parse(_OxygenSaturationInput.text);
        _RespitoryRate = int.Parse(_RespitoryRateInput.text);
        _CapillaryRefillTime = int.Parse(_CapillaryRefillTimeInput.text);
        _PupilSize = int.Parse(_PupilSizeInput.text);
        _Temperature = float.Parse(_TemperatureInput.text);
        _GlucoseLevel = float.Parse(_GlucoseLevelInput.text);
        _SkinColourDescription = _SkinColourInput.text;
        _DepthOfRespiration = _DepthOfRespirationInput.text;
        _ChestSymmetry = _ChestSymmetryInput.text;
        _AccessoryMusclesUsed = _AccessoryMusclesInput.text;
        _PainDescription = _PainInput.text;
        _ChestNoiseDescription = _ChestNoiseInput.text;
        Debug.Log(_HeartBeatsPM);

    }
    void AddNewPatient()
    {
        // update patient system with new values
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
