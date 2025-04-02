using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrampolineApplyForce : MonoBehaviour
{
    [SerializeField] OSC osc;
    [SerializeField] float forceScalar = 50f;
    [SerializeField] bool readFromAudio;
    [SerializeField] bool invertInput = true;
    [SerializeField] int sampleRange;
    [SerializeField] float weightPerSample = .2f;
    [SerializeField] float defaultVel = 0.5f;
    float sampleProduct;
    bool canPlay = true; // Unused
    AudioSource mySource;
    AudioClip myClip;
    float[] data;
    [SerializeField] float threshold = 0.5f;
    
    void Start() {
        osc.SetAddressHandler("/obsidian/hwout/midi1", OSCHandler);
        mySource = GetComponent<AudioSource>();
        data = new float[sampleRange];
        if (readFromAudio) {
            mySource.Play();
        }
        sampleProduct = sampleRange * weightPerSample;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ApplyForce(invertInput ? -defaultVel : defaultVel);
        }
        // - - - Delecluse interpreter - - - //
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
    }

    void OSCHandler(OscMessage message) {
		Debug.Log("Message: " + message.ToString());
        string[] msg = message.ToString().Split(" ");
		// Messages take the form /obsidian/hwout/midi1 <command> <velocity> <note> <transpose> <channel>
        // Here, we care about the command (play vs. stop), the velocity (0.0 to 1.0), and the note (60, 62, or 64)
        
        if (msg[1] == "play") {
            float vel = float.Parse(msg[2]);
            ApplyForce(invertInput ? -vel : vel);
        }
	}

    void ApplyForce(float myVel) {
        GetComponent<Rigidbody>().AddForce(forceScalar * myVel * Vector3.up);
    }
}
