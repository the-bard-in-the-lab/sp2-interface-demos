using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GraphHandler : InputHandler_Generic
{
    IntervalGrapher myGrapher;
    void Start()
    {
        OSCSetup();
        myGrapher = GetComponent<IntervalGrapher>();
    } 
    void Update() {
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

