using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateSettings : MonoBehaviour
{
    [SerializeField] private TMP_InputField portField;
    [SerializeField] private TMP_InputField oscAddressField;
    void OnEnable()
    {
        portField.text = GlobalData.port.ToString();
        oscAddressField.text = GlobalData.OSCAddress;
    }
    public void UpdateSettings() {
        GlobalData.setPort(int.Parse(portField.text));
        GlobalData.setOSCAddress(oscAddressField.text);
    }
}
