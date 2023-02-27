using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateArm : MonoBehaviour
{
    public void UseArm(PlayerController playerController, ToolsBase toolUsed)
    {
        GameObject.FindGameObjectWithTag("PatientArm").GetComponent<PatientArm>().OpenInteractionWheel(playerController, toolUsed);
    }
}
