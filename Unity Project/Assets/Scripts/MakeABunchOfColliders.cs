using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeABunchOfColliders : MonoBehaviour
{
    public GameObject template;
    public int howMany = 50;
    void Start()
    {
        for (int i = 0; i < howMany; i ++) {
            GameObject newBox = Instantiate(template, transform);
            newBox.transform.position = transform.position;
            newBox.transform.rotation = Quaternion.Euler(0f, i * (360.0f / howMany), 0f);
            newBox.SetActive(true);
        }
    }
}
