using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ExitDoor : MonoBehaviour
{
    [SerializeField]
    GameObject _ExitConfirm, _ResultsScreen;

    [SerializeField]
    TextMeshProUGUI _TimerText;

    public void QuitGame()
    {
        ScoringSystem._Instance.GenerateFeedback();
        _ExitConfirm.SetActive(false);
        _ResultsScreen.SetActive(true);
        GetComponent<Timer>().StopTimer();
        _TimerText.text = "Time Taken: " + Mathf.RoundToInt(GetComponent<Timer>()._CurrTime).ToString();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitTab()
    {
        _ExitConfirm.SetActive(true);
        DataManager._Data._WheelOpen = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MenuOpen()
    {
        SceneManager.LoadScene(0);
    }


    public void UnPause()
    {
        _ExitConfirm.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
