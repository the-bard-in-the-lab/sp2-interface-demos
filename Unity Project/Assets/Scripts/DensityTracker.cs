using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DensityTracker : MonoBehaviour
{
    List<double> impulses;
    [SerializeField] OSC osc;
    [SerializeField] [Range(0.01f, 1)] float timeSpan;
    double myDensity;
    Slider mySlider;
    void Start()
    {
        osc.SetAddressHandler("/obsidian/hwout/midi1", MIDIEventHandler);
        impulses = new List<double>();
        mySlider = GetComponent<Slider>();
    }

    
    void MIDIEventHandler(OscMessage message) {
        Debug.Log("Message: " + message.ToString());
        string[] msg = message.ToString().Split(" ");
		// Messages take the form /obsidian/hwout/midi1 <command> <velocity> <note> <transpose> <channel>
        // Here, we care about the command (play vs. stop), the velocity (0.0 to 1.0), and the note (60, 62, or 64)
        
        if (msg[1] == "play") {
            // The user has played a note
            //if (int.Parse(msg[3]) == 68) {} //Parse what note it is
            impulses.Add(AudioSettings.dspTime);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Cull expired values
        double myTime = AudioSettings.dspTime;
        
        // Remove until everything else is good.
        for (int i = 0; i < impulses.Count; i ++) {
            if (myTime > impulses[0] + timeSpan) {
                impulses.RemoveAt(0);
            }
            else {
                break;
            }
        }
        
        myDensity = 0;
        if (impulses.Count > 0) {
            myDensity = impulses.Count / timeSpan;
        }

        mySlider.value = (float) myDensity;
    }
}
