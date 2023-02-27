using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolsBase : MonoBehaviour
{
    [SerializeField]
    private Sprite _ToolImage;
    public string _ToolName;
    private bool _IsPickedUp;
    private int _UsePoint;

    public virtual void ToolInteraction()
    {

    }

    public void PickupTool()
    {
        // player tool
        gameObject.SetActive(false);
        _IsPickedUp = true;
        GameObject.FindGameObjectWithTag("ToolImageSlot").GetComponent<Image>().sprite = _ToolImage;
    }

    public void DropTool()
    {
        Debug.Log("drop");
        // player tool = null
        gameObject.SetActive(true);
        _IsPickedUp = false;
        GameObject.FindGameObjectWithTag("ToolImageSlot").GetComponent<Image>().sprite = null;
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
