using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MetPingData : MonoBehaviour
{
    [SerializeField] AudioClip[] pingArray;
    public void SetPing(int ping, double time) {
        AudioSource mySource = GetComponent<AudioSource>();
        mySource.clip = pingArray[ping];
        if (ping == 7) {
            mySource.volume = 0.2f;
        }
        mySource.PlayScheduled(time);
    }
}
