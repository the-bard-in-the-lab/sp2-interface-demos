using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopcornHandler : InputHandler_Generic
{

    [SerializeField] float forceScalar = 50f;
    [SerializeField] float radius = 4f; // The radius within which we spawn new popcorn pieces
    [SerializeField] bool readFromAudio;
    [SerializeField] int sampleRange;
    [SerializeField] float weightPerSample = .2f;
    [SerializeField] float threshold = 0.5f;
    [SerializeField] Rigidbody[] zones; // The zone objects in the scene. Treated as an array in case we ever want more than two.
    [SerializeField] GameObject prefab; // The popcorn prefab
    float sampleProduct;
    /*
    sampleProduct is a rough estimate of the sum of the magnitude of the
    samples in a short snippet of audio. We compare the actual sum to this
    value when calculating how much force a note should be given.
    */
    AudioSource mySource;
    AudioClip myClip;
    float[] data;
    
    void Start()
    {
        OSCSetup(); // (See note in InputHandler_Generic about OSCSetup)
        mySource = GetComponent<AudioSource>();
        myClip = mySource.clip;
        sampleProduct = sampleRange * weightPerSample;
    } 

    void Update() {
        if (readFromAudio) {
            InterpretAudio();
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                InputHandler("play", 0.5f, 60);
            }
        }
    }

    void InterpretAudio() {
        // - - - Audio Interpreter - - - //
        // This is for reading from audio.
        if (readFromAudio) {
            myClip = mySource.clip;
            int myTime = mySource.timeSamples - sampleRange;
            if (myTime < 0) {
                myTime = 0;
            }
            myClip.GetData(data, myTime);
            float sum = 0f;
            foreach (var i in data) {
                sum += Mathf.Abs(i);
            }
            
            if (sum > threshold) {
                foreach (var zone in zones) {
                    zone.AddForce(forceScalar * (forceScalar / sampleProduct) * Vector3.up);
                }
            }
        }
    }

    protected override void InputHandler(string command, float velocity, int note) {
        Debug.Log("This is the popcorn handler version of the method.");
        Debug.Log($"Message: {command} {velocity} {note}");
        
        
        int zone = note == 60 ? 0 : 1; // If we are in the center of the drum, activate the center zone
        zones[zone].AddForce(forceScalar * velocity * Vector3.up);
        Transform spawnerTransform = GameObject.Find("Spawner").transform;
        GameObject newPrefab = Instantiate(prefab, spawnerTransform);
        Vector2 unitpos = Random.insideUnitCircle;
        newPrefab.transform.position = spawnerTransform.position + new Vector3(unitpos.x * radius, 0, unitpos.y * radius);
        newPrefab.transform.rotation = Random.rotationUniform;
        newPrefab.SetActive(true);
    }
}

