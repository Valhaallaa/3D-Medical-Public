using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InformationBook : MonoBehaviour
{
    [SerializeField]
    GameObject _ATab, _BTab, _CTab, _DTab, _ETab;

    private int _APage = 0, _BPage = 0, _CPage = 0, _DPage = 0;

    [SerializeField]
    GameObject[] AArray,BArray,CArray,DArray;

    public void ANextPage()
    {
        _APage++;
        if (_APage >= AArray.Length - 1)
            _APage = AArray.Length - 1;
        for (int i = 0; i < AArray.Length; i++)
        {
            AArray[i].SetActive(false);
        }
        AArray[_APage].SetActive(true);
    }

    public void APreviousPage()
    {
        _APage--;
        if (_APage <= 0)
            _APage = 0;
        for (int i = 0; i < AArray.Length; i++)
        {
            AArray[i].SetActive(false);
        }
        AArray[_APage].SetActive(true);
    }

    public void BNextPage()
    {
        _BPage++;
        if (_BPage >= BArray.Length - 1)
            _BPage = BArray.Length - 1;
        for (int i = 0; i < BArray.Length; i++)
        {
            BArray[i].SetActive(false);
        }
        BArray[_BPage].SetActive(true);
    }

    public void BPreviousPage()
    {
        _BPage--;
        if (_BPage <= 0)
            _BPage = 0;
        for (int i = 0; i < BArray.Length; i++)
        {
            BArray[i].SetActive(false);
        }
        BArray[_BPage].SetActive(true);
    }

    public void CNextPage()
    {
        _CPage++;
        if (_CPage >= CArray.Length - 1)
            _CPage = CArray.Length - 1;
        for (int i = 0; i < CArray.Length; i++)
        {
            CArray[i].SetActive(false);
        }
        CArray[_CPage].SetActive(true);
    }

    public void CPreviousPage()
    {
        _CPage--;
        if (_CPage <= 0)
            _CPage = 0;
        for (int i = 0; i < CArray.Length; i++)
        {
            CArray[i].SetActive(false);
        }
        CArray[_CPage].SetActive(true);
    }

    public void DNextPage()
    {
        _DPage++;
        if (_DPage >= DArray.Length - 1)
            _DPage = DArray.Length - 1;
        for (int i = 0; i < DArray.Length; i++)
        {
            DArray[i].SetActive(false);
        }
        DArray[_DPage].SetActive(true);
    }

    public void DPreviousPage()
    {
        _DPage--;
        if (_DPage <= 0)
            _DPage = 0;
        for (int i = 0; i < DArray.Length; i++)
        {
            DArray[i].SetActive(false);
        }
        DArray[_DPage].SetActive(true);
    }

    public void ATab()
    {
        _ATab.SetActive(true);
        _BTab.SetActive(false);
        _CTab.SetActive(false);
        _DTab.SetActive(false);
        _ETab.SetActive(false);
    }

    public void BTab()
    {
        _ATab.SetActive(false);
        _BTab.SetActive(true);
        _CTab.SetActive(false);
        _DTab.SetActive(false);
        _ETab.SetActive(false);
    }
    public void CTab()
    {
        _ATab.SetActive(false);
        _BTab.SetActive(false);
        _CTab.SetActive(true);
        _DTab.SetActive(false);
        _ETab.SetActive(false);
    }
    public void DTab()
    {
        _ATab.SetActive(false);
        _BTab.SetActive(false);
        _CTab.SetActive(false);
        _DTab.SetActive(true);
        _ETab.SetActive(false);
    }
    public void ETab()
    {
        _ATab.SetActive(false);
        _BTab.SetActive(false);
        _CTab.SetActive(false);
        _DTab.SetActive(false);
        _ETab.SetActive(true);
    }

    public void CloseBook()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().enabled = true;
        if(DataManager._Data != null)
            DataManager._Data._WheelOpen = false;
    }

}
