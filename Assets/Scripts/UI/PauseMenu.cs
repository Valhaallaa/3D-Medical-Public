using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _Clipboard, _HowToPlayBook, _ContolTab, _NavigationTab, _ToolsTab, _Credits,_Background,_ExitConfirm;

    [SerializeField]
    GameObject[] NavigationArray;
    private int PageID = 0;
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        _Clipboard.SetActive(false);
        _HowToPlayBook.SetActive(true);
    }

    public void ControlsTab()
    {
        _ContolTab.SetActive(true);
        _NavigationTab.SetActive(false);
        _ToolsTab.SetActive(false);
    }

    public void NavigationTab()
    {
        _ContolTab.SetActive(false);
        _NavigationTab.SetActive(true);
        _ToolsTab.SetActive(false);
    }

    public void ToolsTab()
    {
        _ContolTab.SetActive(false);
        _NavigationTab.SetActive(false);
        _ToolsTab.SetActive(true);
    }

    public void ExitTab()
    {
        _ContolTab.SetActive(true);
        _NavigationTab.SetActive(false);
        _ToolsTab.SetActive(false);
        _HowToPlayBook.SetActive(false);
        _Credits.SetActive(false);
        _Clipboard.SetActive(true);
        _ExitConfirm.SetActive(false);

    }

    public void NextPage()
    {
        PageID++;
        if (PageID >= NavigationArray.Length - 1)
            PageID = NavigationArray.Length - 1;
        for (int i = 0; i < NavigationArray.Length; i++)
        {
            NavigationArray[i].SetActive(false);
        }
        NavigationArray[PageID].SetActive(true);
    }

    public void PreviousPage()
    {
        PageID--;
        if (PageID <= 0)
            PageID = 0;
        for (int i = 0; i < NavigationArray.Length; i++)
        {
            NavigationArray[i].SetActive(false);
        }
        NavigationArray[PageID].SetActive(true);
    }

    public void ConfirmExit()
    {
        _ExitConfirm.SetActive(true);
    }
    public void Credits()
    {
        _Credits.SetActive(true);
    }

    public void UnPause()
    {
        DataManager._Data._WheelOpen = false;
        _Background.SetActive(false);
        _Clipboard.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !DataManager._Data._WheelOpen)
            {
            _Background.SetActive(true);
            _Clipboard.SetActive(true);
            Time.timeScale = 0;
            DataManager._Data._WheelOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
