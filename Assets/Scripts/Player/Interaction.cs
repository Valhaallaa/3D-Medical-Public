using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interaction : MonoBehaviour
{
    public GameObject _InteractedObject;
    [SerializeField]
    float _InteractionDistance;
    ToolsBase _ToolHeld;
    [SerializeField]
    TextMeshProUGUI _ToolUI;
    [SerializeField]
    Material _Highlight;
    Material _StoredMat;
    void InteractionRaycast()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;




        if (Physics.Raycast(ray, out hit, _InteractionDistance))
        {



            if (hit.collider != null && hit.transform.gameObject.tag != "Room")
            {
                if (_InteractedObject != hit.collider.gameObject)
                {
                    if (_InteractedObject != null)
                    {
                        /*
                        Material[] Materials = new Material[1];
                        Materials[0] = _StoredMat;
                        _StoredMat = null;

                        _InteractedObject.GetComponent<MeshRenderer>().materials = Materials;
                        _InteractedObject = null;*/
                    }
                }
                _InteractedObject = hit.collider.gameObject;

                // Highlight section
                if (_InteractedObject.tag == "HelpButton" | _InteractedObject.tag == "InfoSheet" | _InteractedObject.GetComponent(typeof(ToolsBase)) as ToolsBase | _InteractedObject.tag == "BodyInteractable")
                {
                    if (_InteractedObject.GetComponent<MeshRenderer>().materials.Length < 2)
                    {
                        /*
                        _StoredMat = _InteractedObject.GetComponent<MeshRenderer>().material;
                        Material[] Materials = new Material[2];
                        Materials[0] = _InteractedObject.GetComponent<MeshRenderer>().material;
                        Materials[1] = _Highlight;
                        _InteractedObject.GetComponent<MeshRenderer>().materials = Materials;
                        */
                    }




                }

                if (_InteractedObject.GetComponent(typeof(ToolsBase)) as ToolsBase)
                {
                    ToolsBase tool = _InteractedObject.GetComponent<ToolsBase>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_ToolHeld)
                        {
                            _ToolHeld.DropTool();
                            _ToolHeld = tool;
                            _ToolHeld.PickupTool();
                        }
                        else
                        {
                            _ToolHeld = tool;
                            _ToolHeld.PickupTool();
                        }
                    }

                    _ToolUI.text = "Click to pick up " + tool._ToolName;
                }
                else if (_InteractedObject.GetComponent<PatientHead>())
                {
                    _ToolUI.text = "Click to interact with the Patient's Head";
                }
                else if (_InteractedObject.GetComponent<PatientChest>())
                {
                    _ToolUI.text = "Click to interact with the Patient's Chest";
                }
                else if (_InteractedObject.GetComponent<PatientArm>() || _InteractedObject.GetComponent<AlternateArm>())
                {
                    _ToolUI.text = "Click to interact with the Patient's Arm";
                }
                else if (_InteractedObject.GetComponent<ExitDoor>())
                {
                    _ToolUI.text = "Click to End Examination";
                }
                else if (_InteractedObject.tag == "HelpButton")
                {
                    _ToolUI.text = "Click to call for help";
                }
                else
                {
                    _ToolUI.text = "";
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (_InteractedObject.GetComponent<PatientHead>())
                    {
                        _InteractedObject.GetComponent<PatientHead>().OpenInteractionWheel(GetComponent<PlayerController>(), _ToolHeld); // passes i the PlayerController and current tool
                    }
                    if (_InteractedObject.GetComponent<PatientChest>())
                    {
                        _InteractedObject.GetComponent<PatientChest>().OpenInteractionWheel(GetComponent<PlayerController>(), _ToolHeld); // passes i the PlayerController and current tool
                    }
                    if (_InteractedObject.GetComponent<PatientArm>())
                    {
                        _InteractedObject.GetComponent<PatientArm>().OpenInteractionWheel(GetComponent<PlayerController>(), _ToolHeld); // passes i the PlayerController and current tool
                    }
                    if (_InteractedObject.GetComponent<AlternateArm>())
                    {
                        _InteractedObject.GetComponent<AlternateArm>().UseArm(GetComponent<PlayerController>(), _ToolHeld);
                        
                    }
                        if (_InteractedObject.tag == "HelpButton")
                        DataManager._Data.CallHelp();
                    if (_InteractedObject.tag == "InfoSheet")
                        GameObject.FindGameObjectWithTag("UIInfoSheet").SetActive(true);
                    if (_InteractedObject.tag == "Door" && !DataManager._Data._WheelOpen)
                        _InteractedObject.GetComponent<ExitDoor>().ExitTab();
                    // could use switch case with typeof() and inherit from a base class in the body part scripts
                }
            }

            else
            {
                _ToolUI.text = "";
                if (_InteractedObject != null)
                {
                    /*
                    Material[] Materials = new Material[1];
                    Materials[0] = _StoredMat;
                    _StoredMat = null;

                    _InteractedObject.GetComponent<MeshRenderer>().materials = Materials;
                    _InteractedObject = null;*/
                }
            }
        }
        else
            _ToolUI.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

            InteractionRaycast();
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // if no wheel, pause
            // if wheel, close wheel
        }
    }
}
