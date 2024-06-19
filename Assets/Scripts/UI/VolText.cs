using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolText : MonoBehaviour {
    Slider slider;
    TextMeshProUGUI volumeText;
    void Start() {
        slider = transform.parent.GetComponent<Slider>();
        volumeText = transform.GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        volumeText.text = "Volume " + (slider.value * 100).ToString("0") + "%";
    }
}
