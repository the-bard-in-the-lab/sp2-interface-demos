using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsOnStart : MonoBehaviour
{
    public GameObject prefab;
    public int number = 100;
    public float positionVariation = 1f;
    public float heightVariation = 1f;
    void Start()
    {
        for (int i = 0; i < number; i ++) {
            SpawnObject(prefab);
        }
    }

    void SpawnObject(GameObject myObject) {
        GameObject newObject = Instantiate(myObject, transform);
        newObject.transform.position = transform.position;
        newObject.transform.position += new Vector3(Random.Range(-positionVariation, positionVariation), Random.Range(-heightVariation, heightVariation), Random.Range(-positionVariation, positionVariation));
        newObject.transform.rotation = Random.rotationUniform;
        newObject.SetActive(true);
    }
}
