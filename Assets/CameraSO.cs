using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CameraSO : ScriptableObject
{
    [Header("General")]
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    public float rotationSpeed, mouvementSpeed;
    [Space]

    [Header("Fov and Clipping")]
    [Range(20, 180)] //slider
    public float fov;
    [Range(0, 10)] //slider
    public float clipping;
    [Space]

    [Header("Collision and Perspective")]
    public bool checkCollision;
    public bool isOrthographic;

    [Header("Zoom")]
    [Range(0,60)] //slider
    public float fovZoom;
    public Vector3 positionZoom;
}
