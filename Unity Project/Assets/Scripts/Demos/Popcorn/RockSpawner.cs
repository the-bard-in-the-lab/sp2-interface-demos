using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] rocks;
    Vector3 spawnSpeed = new Vector3(0, -1f, 0);
    [SerializeField] OSC osc;
    [SerializeField] float rockSizeMin;
    [SerializeField] float rockSizeMax;


    void Start() {
        osc.SetAddressHandler("/obsidian/hwout/midi1", RockHandler);
    }
    
    void RockHandler(OscMessage message) {
		Debug.Log("Message: " + message.ToString());
        string[] msg = message.ToString().Split(" ");
		// Messages take the form /obsidian/hwout/midi1 <command> <velocity> <note> <transpose> <channel>
        // Here, we care about the command (play vs. stop), the velocity (0.0 to 1.0), and the note (60, 62, or 64)
        
        if (msg[1] == "play") {
            // The user has played a note
            //if (int.Parse(msg[3]) == 68) {} //Parse what note it is
            float velAsFloat = float.Parse(msg[2]);
            float myScale = (rockSizeMax - rockSizeMin) * velAsFloat + rockSizeMin;
            SpawnARock(myScale);
        }
	}

    public void SpawnARock(float scale)
    {
        GameObject newRock = Instantiate(rocks[Random.Range(0, rocks.Length)], gameObject.transform); //Spawns a new rock as child of spawn point
        newRock.transform.position = transform.position;
        newRock.transform.localScale = new Vector3(scale, scale, scale);
        Rigidbody rockBody = newRock.GetComponent<Rigidbody>();
        rockBody.mass = scale;
        rockBody.velocity = spawnSpeed;
        rockBody.rotation = Random.rotationUniform;
    }
}
