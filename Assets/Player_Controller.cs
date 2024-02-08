using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller instance;

    public float speed = 8;
    private Vector2 direction;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {

        transform.position += transform.right * (speed * Time.deltaTime * direction.x);
        if(!CameraSettings.instance.is2D)
        {
            transform.position += transform.forward * (speed * Time.deltaTime * direction.y);
        }
        //transform.Rotate(0, CameraSettings.instance.rotationSpeed * Time.deltaTime * direction.x, 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        Debug.Log("tu appuie");
    }
}

