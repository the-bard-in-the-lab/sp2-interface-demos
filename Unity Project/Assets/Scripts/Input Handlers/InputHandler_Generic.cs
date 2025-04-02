using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputHandler_Generic : MonoBehaviour
{
    OSC osc;
    void Start() {
        OSCSetup();
	}

    protected void OSCSetup() {
        osc = GlobalData.getOSC();
        //osc.inPort = GlobalData.port;
        osc.SetAddressHandler(GlobalData.OSCAddress, OSCHandler);
    }
    void OSCHandler(OscMessage message) {
        
        //Debug.Log("Message: " + message.ToString());
        string[] msg = message.ToString().Split(" ");
		// Sensory Percussion 2 OSC messages take the form /obsidian/hwout/midi1 <command> <velocity> <note> <transpose> <channel>
        // Typically, we care about the command (play vs. stop), the velocity (0.0 to 1.0), and the note (as a MIDI number)
        string command = msg[1];
        float velocity = float.Parse(msg[2]);
        int note = int.Parse(msg[3]);
        // int transpose = int.Parse(msg[4]);
        // int channel = int.Parse(msg[5]);
        InputHandler(command, velocity, note);
    }

    protected virtual void InputHandler(string command, float velocity, int note) {
        Debug.Log("If you see this, you have a child class that needs to override this method.");
        Debug.Log($"Message: {command} {velocity} {note}");
    }
}
