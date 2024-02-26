using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
    bool isFPS, isTPS, isScroller;

    [Header("Parameters")]
    [HideInInspector]public float rotationSpeed;
    [HideInInspector] public bool is2D;
    private float mouvementSpeed;
    public GameObject collision;
    bool isZoom = false;

    [Header("Mouse Parameters")]
    public float sensitivity = 15f;
    Vector2 mousePos;
    public GameObject mover;
    public Vector3 deltaMove;
    public float speed=1;

    public float pLerp = 0.02f;
    public float rLerp = 0.01f;


    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {        
        cameraActive = cameraFPS;
        ChangeValue();
    }

    public void Update()
    {
        if (isFPS)
        {
            Cursor.lockState = CursorLockMode.Locked;
            mousePos.x += Input.GetAxis("Mouse X") * sensitivity;
            mousePos.y += Input.GetAxis("Mouse Y") * sensitivity;
            mover.transform.localRotation = Quaternion.Euler(0, mousePos.x, 0);
            transform.localRotation = Quaternion.Euler(-mousePos.y, 0, 0);

            deltaMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
            mover.transform.Translate(deltaMove);
        }
        else if(isTPS)
        {
            mousePos.x += Input.GetAxis("Mouse X") * sensitivity;
            mousePos.y += Input.GetAxis("Mouse Y") * sensitivity;
            mover.transform.localRotation = Quaternion.Euler(0, mousePos.x, 0);
            transform.localRotation = Quaternion.Euler(-mousePos.y, 0, 0);

            deltaMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
            mover.transform.Translate(deltaMove);

            transform.position = Vector3.Lerp(transform.position, mover.transform.position, pLerp);
            transform.rotation = Quaternion.Lerp(transform.rotation,mover.transform.rotation, rLerp);
        }
    }


    public void ChangeCameraToFPS(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            cameraActive = cameraFPS;
            isFPS = true;
            isTPS = false;
            isScroller = false;
            ChangeValue();
        }
        else { return; }
    }

    public void ChangeCameraToTPS(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            cameraActive = cameraTPS;
            isFPS = false;
            isTPS = true;
            isScroller = false;
            ChangeValue();
        }
        else { return; }
    }

    public void ChangeCameraToScroller(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoom)
        {
            cameraActive = cameraScroller;
            isFPS = false;
            isTPS = false;
            isScroller = true;
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
