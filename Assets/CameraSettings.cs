using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSettings : MonoBehaviour
{
    public static CameraSettings instance;


    public Camera mainCamera;
    private CameraSO cameraActive;

    [Header("Scriptable Object : Camera")]
    public CameraSO cameraFPS;
    public CameraSO cameraTPS;
    public CameraSO cameraScroller;

    [Header("Parameters")]
    [HideInInspector]public float rotationSpeed;
    [HideInInspector] public bool is2D;
    private float mouvementSpeed;
    public GameObject collision;
    bool isZoom = false;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        cameraActive = cameraFPS;
        ChangeValue();
    }

    public void ChangeCameraToFPS(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            cameraActive = cameraFPS;
            ChangeValue();
        }
        else { return; }
    }

    public void ChangeCameraToTPS(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            cameraActive = cameraTPS;
            ChangeValue();
        }
        else { return; }
    }

    public void ChangeCameraToScroller(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            cameraActive = cameraScroller;
            ChangeValue();
        }
        else { return; }
    }

    public void ChangeValue()
    {
        mainCamera.transform.localPosition = cameraActive.offsetPosition;
        mainCamera.transform.localRotation = Quaternion.Euler(cameraActive.offsetRotation);
        rotationSpeed = cameraActive.rotationSpeed;
        mouvementSpeed = cameraActive.mouvementSpeed;

        mainCamera.fieldOfView = cameraActive.fov;

        mainCamera.nearClipPlane = cameraActive.clipping;

        if(cameraActive.isOrthographic)
        {
            mainCamera.orthographic = true;
            is2D = true;
        }
        else
        {
            mainCamera.orthographic = false;
            is2D = false;
        }

        if(cameraActive.checkCollision)
        {
            collision.gameObject.SetActive(true);
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
    
    public void ZoomIn(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            mainCamera.fieldOfView = cameraActive.fovZoom;
            mainCamera.transform.localPosition = cameraActive.positionZoom;
            isZoom = true;
        }
        else if (context.performed && isZoom)
        {
            mainCamera.fieldOfView = cameraActive.fov;
            mainCamera.transform.localPosition =  cameraActive.offsetPosition;
            isZoom = false;
        }
    }
}
