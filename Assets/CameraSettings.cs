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
    public CameraStatus cameraStatus;
    private CameraSO cameraActive;

    [Header("Scriptable Object : Camera")]
    public CameraSO cameraFPS;
    public CameraSO cameraTPS;
    public CameraSO cameraScroller;

    [Header("Parameters")]
    public float rotationSpeed;
    public float mouvementSpeed;
    public GameObject collision;

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

                break;

            case CameraStatus: 
                cameraActive = cameraScroller;

                break;

        }
    }


    public void ChangeValue()
    {
        Camera.main.transform.position += cameraActive.offsetPosition;
        rotationSpeed = cameraActive.rotationSpeed;
        mouvementSpeed = cameraActive.mouvementSpeed;

        Camera.main.fieldOfView = cameraActive.fov;

        Camera.main.nearClipPlane = cameraActive.clipping;

        if(cameraActive.isOrthographic)
        {
            Camera.main.orthographic = true;
        }
        else
        {
            Camera.main.orthographic = false;
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
        Vector3 previousPos = Camera.main.transform.position;
        if (context.started)
        {
            Camera.main.fieldOfView = cameraActive.fovZoom;
            Camera.main.transform.position = cameraActive.positionZoom;
        }
        else if (context.canceled)
        {
            Camera.main.fieldOfView = cameraActive.fov;
            Camera.main.transform.position = previousPos;
        }
    }
}
