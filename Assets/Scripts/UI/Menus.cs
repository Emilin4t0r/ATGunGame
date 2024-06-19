using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menus : MonoBehaviour {
    public TextMeshProUGUI waves, kills;
    public GameObject pauseMenu;
    public AudioMixer mixer;
    GameObject fader;
    int iKills, iWaves;
    public static bool paused;
    void Start() {
        fader = transform.GetChild(0).gameObject;
        TogglePauseMenu();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }
    }

    void FixedUpdate() {
        if (fader.activeSelf) {
            //iKills = GameObject.Find("BulletSpawn").GetComponent<GunShoot>().kills;
            iWaves = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().currentWave;
            waves.text = "You made it to wave " + iWaves;
            kills.text = "And got " + iKills + " kills";
        }
    }

    public void ToMainMenu() {
        EnemyManager.enemiesLeft = 0;
        SceneManager.LoadScene("MenuScene");
    }

    public void TogglePauseMenu() {
        if (pauseMenu.activeSelf) {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        } else {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
    }

    public void SetMixerLevel(float sliderValue) {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void Retry() {
        EnemyManager.enemiesLeft = 0;
        SceneManager.LoadScene("GameScene");
    }
    public void Quit() {
        Application.Quit();
    }

    public void PlayHover() {
        //AudioFW.Play("MenuHover");
    }
    public void PlayClick() {
        //AudioFW.Play("MenuClick");
    }
}
