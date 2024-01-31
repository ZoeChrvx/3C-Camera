using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSO : ScriptableObject
{
    public Vector3 offsetPosition;
    public GameObject pivotPoint;
    public float rotationSpeed, mouvementSpeed;
    [Range(20, 180)] //slider
    public float fov;
    public float fovZoom;
    public Vector3 positionZoom;
    public bool checkCollision;
    public float clipping;
    public bool isPerspective;
}
