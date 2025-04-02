using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GlobalData : MonoBehaviour
{
    public static int port = 9001;
    private static int defaultPort = 9001;
    public static string OSCAddress = "/obsidian/hwout/midi1";
    private static string defaultAddress = "/obsidian/hwout/midi1";
    public static OSC osc;
    

    public static void setPort(int newPort) {
        if (newPort <= 0) {
            Debug.LogError("Invalid port; using default port of " + defaultPort);
            port = defaultPort;
        }
        else if (newPort <= 1023) {
            Debug.LogError("This port is reserved; using default port of " + defaultPort);
            port = defaultPort;
        }
        else if (newPort > 65535) {
            Debug.LogError("Invalid port; using default port of " + defaultPort);
            port = defaultPort;
        }
        else {
            port = newPort;
        }
    }

    public static void setOSCAddress(string newAddress) {
        // TODO: Confirm the user isn't trying to break the system
        OSCAddress = newAddress;

        // Debug.LogError("Invalid OSC Address; using default address of " + defaultAddress);
        // OSCAddress = defaultAddress;
    }

    public void setAddressFromField() {
        setOSCAddress(GetComponent<TMP_InputField>().text);
    }

    public void setPortFromField() {
        setPort(int.Parse(GetComponent<TMP_InputField>().text));
    }

    public static OSC getOSC() {
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();
        Debug.Log($"Found object {osc}");
        return osc;
    }

}
