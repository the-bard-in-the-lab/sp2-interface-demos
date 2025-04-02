using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopcornHandler : InputHandler_Generic
{

    [SerializeField] float forceScalar = 50f;
    [SerializeField] bool readFromAudio;
    [SerializeField] int sampleRange;
    [SerializeField] float weightPerSample = .2f;
    
    //[SerializeField] float defaultVel = 0.5f;
    [SerializeField] float radius = 4f;
    float sampleProduct;
    //bool canPlay = true; // Unused
    AudioSource mySource;
    AudioClip myClip;
    float[] data;
    [SerializeField] float threshold = 0.5f;
    public Rigidbody[] zones;
    public GameObject prefab;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InputHandler("play", 0.5f, 60);
        }
        // - - - Delecluse interpreter - - - //
        /*
        if (readFromAudio) {
            myClip = mySource.clip;
            int myTime = mySource.timeSamples - sampleRange;
            if (myTime < 0) {
                myTime = 0;
            }
            myClip.GetData(data, myTime);
            float sum = 0f;
            foreach (var i in data) {
                sum += Math.Abs(i);
            }
            
            if (sum > threshold) {
                if (canPlay) {
                    //canPlay = false;
                    ApplyForce(sum / sampleProduct);
                    if (sum / sampleProduct > 0.5f) {
                        Debug.Log(sum / sampleProduct);
                    }
                }
            }
            else {
                canPlay = true;
            }
        }
        */
    }


    protected override void InputHandler(string command, float velocity, int note) {
        Debug.Log("This is the popcorn handler version of the method.");
        Debug.Log($"Message: {command} {velocity} {note}");
        
        
        int zone = note == 60 ? 0 : 1;
        zones[zone].AddForce(forceScalar * velocity * Vector3.up);
        Transform spawnerTransform = GameObject.Find("Spawner").transform;
        GameObject newPrefab = Instantiate(prefab, spawnerTransform);
        Vector2 unitpos = Random.insideUnitCircle;
        newPrefab.transform.position = spawnerTransform.position + new Vector3(unitpos.x * radius, 0, unitpos.y * radius);
        newPrefab.transform.rotation = Random.rotationUniform;
        newPrefab.SetActive(true);
    }
}

