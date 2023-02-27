using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FeedbackHandler : MonoBehaviour
{

    public static FeedbackHandler _Handler;

    [SerializeField]
    TextMeshProUGUI Title,Description;
    [SerializeField]
    GameObject Background;



    [SerializeField]
    private float _CountDownTimer;
    private float _CurrentTime;

    public void SetText(string TitleText,string DescriptionText)
    {
        
        Title.text = TitleText;
        Description.text = DescriptionText;
        Background.SetActive(true);
        _CurrentTime = 0;

    }

    private void ClearFeedback()
    {

        
        Title.text = " ";
        Description.text = " ";
        Background.SetActive(false);
    }

    void Start()
    {
        if (_Handler == null)
            _Handler = this;
        else
            Destroy(gameObject);
        ClearFeedback();
    }

    private void Update()
    {
        _CurrentTime += Time.deltaTime;
        if (_CurrentTime >= _CountDownTimer)
            ClearFeedback();
    }

}
