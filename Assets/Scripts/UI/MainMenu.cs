using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button start, help, quit;
    public GameObject menuPage, helpPage;

    public void Help() {
        helpPage.SetActive(true);
        menuPage.SetActive(false);
    }
    public void StartGame() {
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
