using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    GameObject _Clipboard, _InformationBook;

    public Animator pressed;
    public bool animation_Bool;

    public void InformationBook()
    {

        if (Input.GetKeyDown(KeyCode.B) && !DataManager._Data._WheelOpen)
        {
            _InformationBook.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DataManager._Data._WheelOpen = true;
            GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }

    public void InforSheet()
    {
        if (animation_Bool == true)
        {
            pressed.Play("Pressed");
        }

        if (Input.GetKeyDown(KeyCode.C) && !DataManager._Data._WheelOpen)
        {
            _Clipboard.SetActive(true);
            pressed.SetTrigger("Press");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DataManager._Data._WheelOpen = true;
            GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }

    public void CloseInfoSheet()
    {
        _Clipboard.SetActive(false);
        Cursor.visible = false;
        DataManager._Data._WheelOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    private void Update()
    {

        InforSheet();

        InformationBook();
        
    }
}
