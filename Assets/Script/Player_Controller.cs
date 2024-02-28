using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller instance;


    public float speed = 8;
    public bool isRunning;
    private Vector2 direction;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        isRunning = false;
    }

    public void Update()
    {

        transform.position += transform.right * (speed * Time.deltaTime * direction.x);
        if(!CameraSettings.instance.is2D)
        {
            transform.position += transform.forward * (speed * Time.deltaTime * direction.y);
        }

        if (!isRunning && CameraSettings.instance.isTPS)
        {
            CameraSettings.instance.mainCamera.transform.localPosition = Vector3.Slerp(CameraSettings.instance.mainCamera.transform.localPosition, CameraSettings.instance.cameraActive.offsetPosition, CameraSettings.instance.pLerp);
            CameraSettings.instance.mainCamera.nearClipPlane = CameraSettings.instance.cameraActive.clipping;
        }
        else if(isRunning && CameraSettings.instance.isTPS)
        {
            CameraSettings.instance.mainCamera.transform.localPosition = Vector3.Slerp(CameraSettings.instance.mainCamera.transform.localPosition, new Vector3(-0.4f, 1.6f, -2f), CameraSettings.instance.pLerp);
            CameraSettings.instance.mainCamera.nearClipPlane = 0.01f;
        }

        if (CameraSettings.instance.isScroller)
        {
            if(direction.x > 0) 
            {
                CameraSettings.instance.mainCamera.transform.localPosition = Vector3.Slerp(CameraSettings.instance.mainCamera.transform.localPosition, CameraSettings.instance.cameraActive.offsetPosition + new Vector3(7f, 0, 0), CameraSettings.instance.pLerp);
            }
            else if(direction.x < 0)
            {
                CameraSettings.instance.mainCamera.transform.localPosition = Vector3.Slerp(CameraSettings.instance.mainCamera.transform.localPosition, CameraSettings.instance.cameraActive.offsetPosition + new Vector3(-7f, 0, 0), CameraSettings.instance.pLerp);
            }
            else if(direction.x == 0)
            {
                CameraSettings.instance.mainCamera.transform.localPosition = Vector3.Slerp(CameraSettings.instance.mainCamera.transform.localPosition, CameraSettings.instance.cameraActive.offsetPosition, CameraSettings.instance.pLerp);
            }
        }
        //transform.Rotate(0, CameraSettings.instance.rotationSpeed * Time.deltaTime * direction.x, 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        
    }

    public void Run(InputAction.CallbackContext context)
    {
        if(context.performed && CameraSettings.instance.isTPS)
        {
            speed = 12;
            isRunning = true;
            
        }
        if (context.canceled && CameraSettings.instance.isTPS)
        {
            speed = 8;
            isRunning = false;
            
        }
    }
}

