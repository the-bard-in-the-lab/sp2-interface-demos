using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class FireworkHandler : MonoBehaviour
{
    public OSC osc;
    public Color[] colors;
    public GameObject firework;
    void Start() {
		osc.SetAddressHandler("/obsidian/hwout/midi1", MidiHandler);
	}

    void MidiHandler(OscMessage message) {
		Debug.Log("Message: " + message.ToString());
        string[] msg = message.ToString().Split(" ");
		// Messages take the form /obsidian/hwout/midi1 <command> <velocity> <note> <transpose> <channel>
        // Here, we care about the command (play vs. stop), the velocity (0.0 to 1.0), and the note (60, 62, or 64)
        
        if (msg[1] == "play") {
            // The user has played a note
            if (int.Parse(msg[3]) == 68) { //68 is G4, the note we're using to represent the pause button
                if (float.Parse(msg[2]) > 0.01) { // Make sure it's not a tick
                    PauseMaybe();
                }
            }
            else {
                // We want to create a new firework
                float size = float.Parse(msg[2]);
                int color = int.Parse(msg[3]);
                CreateFirework(size, color);
            }
        }
	}

    void CreateFirework(float size, int color) {
        int ind = (color - 60) / 2; // Colors are now 0, 1, 2
        Color myColor = colors[ind];
        int mySize = (int) (size * 30) + 1;
        
        GameObject myFirework = Instantiate(firework);

        var main = myFirework.GetComponent<ParticleSystem>().main;
        main.startColor = myColor;
        
        Debug.Log("Created firework with color " + ind + " and count " + mySize);
        myFirework.GetComponent<ParticleSystem>().Emit(mySize);
        Destroy(myFirework, 5f);
    }
    void PauseMaybe() {
        CreateFirework(1, 66);
    }
}