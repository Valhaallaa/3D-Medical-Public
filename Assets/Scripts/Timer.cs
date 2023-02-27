using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _TimerText;

    public float _CurrTime;
    private bool _TimerStopped = false;


    public void StopTimer()
    {
        _TimerStopped = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_TimerStopped)
        {
            _CurrTime += Time.deltaTime;

           // _TimerText.text = "Time Taken: " + Mathf.Round(_CurrTime).ToString();
        }
    }
}
