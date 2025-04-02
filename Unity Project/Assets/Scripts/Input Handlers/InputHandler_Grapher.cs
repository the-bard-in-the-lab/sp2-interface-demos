using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GraphHandler : InputHandler_Generic
{
    // This is the input handler for the Grapher demo.
    IntervalGrapher myGrapher;
    void Start()
    {
        OSCSetup(); // (See note in InputHandler_Generic about OSCSetup)
        myGrapher = GetComponent<IntervalGrapher>();
    } 
    void Update() {
        // Mimic drum input using the space bar
        if (Input.GetKeyDown(KeyCode.Space)) {
            InputHandler("play", 0.5f, 60);
        }
    }

    protected override void InputHandler(string command, float velocity, int note) {
        Debug.Log("This is the graph handler version of the method.");
        Debug.Log($"Message: {command} {velocity} {note}");
        if (command.Equals("play")) {
            myGrapher.AddNewImpulse();
        }
    }
}

