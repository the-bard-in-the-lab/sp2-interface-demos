using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TMP_Slider : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void UpdateText() {
        text.text = GetComponent<Slider>().value.ToString();
    }
}
