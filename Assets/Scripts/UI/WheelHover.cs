using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHover : MonoBehaviour
{
    [SerializeField]
    float _IncreasedScale;
    public void IncreaseSize()
    {
        gameObject.transform.localScale = new Vector3(_IncreasedScale, _IncreasedScale, _IncreasedScale);

    }

    public void DecreaseSize()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);


    }
}
