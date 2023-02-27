using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject _Clipboard, _HowToPlayBook, _ContolTab, _NavigationTab, _ToolsTab, _Credits;

    [SerializeField]
    GameObject[] NavigationArray;
    private int PageID = 0;
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
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

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }
    public void Credits()
    {
        _Credits.SetActive(true);
    }

    public void Exitgame()
    {
        Application.Quit();
    }
}
