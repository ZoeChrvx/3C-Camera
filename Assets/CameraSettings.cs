using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public enum CameraStatus
{
    FPS, TPS, Scroller
}
public class CameraSettings : MonoBehaviour
{
    public Camera mainCamera;
    public CameraStatus cameraStatus;
    private CameraSO cameraActive;

    [Header("Scriptable Object : Camera")]
    public CameraSO cameraFPS;
    public CameraSO cameraTPS;
    public CameraSO cameraScroller;

    [Header("Parameters")]
    [HideInInspector]public float rotationSpeed;
    private float mouvementSpeed;
    public GameObject collision;

    public static CameraSettings instance;

    public void Awake()
    {
        instance = this;
    }

    public void ChangeCameraToFPS(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraStatus = CameraStatus.FPS;
        }
    }

    public void ChangeCameraToTPS(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraStatus = CameraStatus.TPS;
        }
    }

    public void ChangeCameraToScroller(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraStatus = CameraStatus.Scroller;
        }
    }


    private void Update()
    {
        switch(cameraStatus)
        {
            case CameraStatus.FPS:
                cameraActive = cameraFPS;
                ChangeValue();
                break;

            case CameraStatus.TPS:
                cameraActive = cameraTPS;
                ChangeValue();
                break;

            case CameraStatus: 
                cameraActive = cameraScroller;
                ChangeValue();
                break;

        }
    }


    public void ChangeValue()
    {
        mainCamera.transform.position = cameraActive.offsetPosition;
        mainCamera.transform.rotation = Quaternion.Euler(cameraActive.offsetRotation);
        rotationSpeed = cameraActive.rotationSpeed;
        mouvementSpeed = cameraActive.mouvementSpeed;

        mainCamera.fieldOfView = cameraActive.fov;

        mainCamera.nearClipPlane = cameraActive.clipping;

        if(cameraActive.isOrthographic)
        {
            mainCamera.orthographic = true;
        }
        else
        {
            mainCamera.orthographic = false;
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
        Vector3 previousPos = mainCamera.transform.position;
        if (context.started)
        {
            mainCamera.fieldOfView = cameraActive.fovZoom;
            mainCamera.transform.position = cameraActive.positionZoom;
        }
        else if (context.canceled)
        {
            mainCamera.fieldOfView = cameraActive.fov;
            mainCamera.transform.position = previousPos;
        }
    }
}
