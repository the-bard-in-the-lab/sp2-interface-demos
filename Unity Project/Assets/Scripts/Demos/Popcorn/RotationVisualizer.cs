using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationVisualizer : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] OSC osc;
    Vector3 myAxis;
    void Start() {
        osc.SetAddressHandler("/obsidian/hwout/midi1", BurgerHandler);
        PickANewAxis();
        PickANewRotation();
    }
    void Update()
    {
        transform.RotateAround(Vector3.zero, myAxis, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) {
            PickANewAxis();
            PickANewRotation();
        }
    }

    void PickANewAxis() {
        myAxis = new Vector3(Random.Range(-1f, 1f),
                             Random.Range(-1f, 1f),
                             Random.Range(-1f, 1f));
    }
    void PickANewRotation() {
        transform.rotation = Random.rotationUniform;
    }

    void BurgerHandler(OscMessage message) {
		Debug.Log("Message: " + message.ToString());
        string[] msg = message.ToString().Split(" ");
		// Messages take the form /obsidian/hwout/midi1 <command> <velocity> <note> <transpose> <channel>
        // Here, we care about the command (play vs. stop), the velocity (0.0 to 1.0), and the note (60, 62, or 64)
        
        if (msg[1] == "play") {
            // The user has played a note
            //if (int.Parse(msg[3]) == 68) {} //Parse what note it is
            PickANewAxis();
            PickANewRotation();
        }
	}
}
