using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CameraSO : ScriptableObject
{
    [Header("General")]
    public Vector3 offsetPosition;
    public GameObject pivotPoint;
    public float rotationSpeed, mouvementSpeed;
    [Space]

    [Header("Fov and Zoom")]
    [Range(20, 180)] //slider
    public float fov;
    public float fovZoom;
    public Vector3 positionZoom;
    [Space]

    [Header("Collision and Perspective")]
    public bool checkCollision;
    public float clipping;
    public bool isPerspective;
}
