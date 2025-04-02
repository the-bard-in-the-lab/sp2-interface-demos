using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinuxScreensaverVibes : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;
    
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
