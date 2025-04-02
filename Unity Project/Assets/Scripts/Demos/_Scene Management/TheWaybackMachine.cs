using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaybackMachine : InputHandler_Generic
{
    [SerializeField] private string sceneTarget;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GoToScene();
        }
    }
    protected override void InputHandler(string command, float velocity, int note) {
        if (command.Equals("play") && note == 71) {
            GoToScene();
        }
    }

    void GoToScene() {
        SceneChanger.GoToScene(sceneTarget);
    }
}
