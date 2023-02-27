using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField]
    GameObject Tick;
    bool Ticked;

    public void Toggle()
    {
        if (Ticked)
        {
            Ticked = false;
            Tick.SetActive(false);
        }
        else
        {
            Ticked = true;
            Tick.SetActive(true);
        }

    }
}
