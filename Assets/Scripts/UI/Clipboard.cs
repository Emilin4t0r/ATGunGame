using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clipboard : MonoBehaviour {

    public static Clipboard instance;
    public GameObject gameInfo;
    public GameObject nextWaveInfo;
    public TextMeshProUGUI waveText, killsText, waveTimerText;

    void Start() {
        instance = this;
        nextWaveInfo.SetActive(false);
    }

    public void ChangeWave(string newText) {
        waveText.text = "Wave: " + newText;
    }
    public void ChangeKills(string newText) {
        killsText.text = "Kills: " + newText;
    }
    public void ChangeWaveTimerTime(string newText) {
        waveTimerText.text = "Next Wave Begins In: " + newText;
    }

    public void ChangeActiveInfo(string info) {
        if (info == "game") {
            nextWaveInfo.SetActive(false);
            gameInfo.SetActive(true);
        } else if (info == "nextwave") {
            nextWaveInfo.SetActive(true);
            gameInfo.SetActive(false);
        }
    }
}
