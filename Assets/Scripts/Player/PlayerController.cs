using System.Collections;

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed = 2f;
    CharacterController _Controller;
    public bool _MovementLocked;
    // Start is called before the first frame update
    void Start()
    {
        _Controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Movement()
    {
        if (!_MovementLocked)
        {
            float X = Input.GetAxisRaw("Horizontal");
            float Z = Input.GetAxisRaw("Vertical");
            Vector3 _Movement = transform.right * X + transform.forward * Z;

            _Controller.Move(_Movement * _MoveSpeed * Time.deltaTime);
            transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
        }
    }


    // Update is called once per frame
    void Update()
    {

        Movement();
        _Controller.Move(new Vector3(0, -9.8f * Time.deltaTime, 0));
    }
}
