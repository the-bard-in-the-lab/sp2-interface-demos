using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] float DeathPlane_y = -2f;
    
    void Update()
    {
        if (transform.position.y < DeathPlane_y) {
            Destroy(gameObject);
        }
    }
}
