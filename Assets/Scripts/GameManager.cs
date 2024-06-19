using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public int lives = 0;
    public int kills = 0;
    public bool gameLost;
    public Animator camAnim, gameAnim, faderAnim;
    public Texture2D cursor, cursorGrab;
    public GameObject fader;
    public GameObject explosion;
    public GameObject mainCam;

    private void Awake() {
        
    }

    void Start() {
        Invoke("SetTimeScale", 0.1f);
        instance = this;
        lives = 3;
    }

    void Update() {        
        if (lives < 1 && !gameLost) {
            StartCoroutine(LoseGame());
        }
        //Grenade follows camera's x movement so that it shows on death regardless of camera side position
        transform.position = new Vector3(mainCam.transform.position.x, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.SetCursor(cursorGrab, new Vector2(250, 250), CursorMode.Auto);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Cursor.SetCursor(cursor, new Vector2(250, 250), CursorMode.Auto);
        }
    }

    public IEnumerator LoseGame() {
        print("You Lose!");
        gameLost = true;
        yield return new WaitForSeconds(2);
        camAnim.SetTrigger("Lose");
        gameAnim.SetTrigger("Lose");
        //AudioFW.Play("LoseGame");
        yield return new WaitForSeconds(1.5f);
        GameObject expl = Instantiate(explosion, transform.GetChild(0).transform.position, explosion.transform.rotation);
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(expl, 0.25f);
        yield return new WaitForSeconds(0.07f);
        fader.SetActive(true);
        faderAnim.SetTrigger("Fade");        
        //AudioFW.StopLoop("Ambience");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    void SetTimeScale() {
        Time.timeScale = 1;
    }

    public void CallShowHitMark(bool hitSound)
    {
        StartCoroutine(ShowHitMark(hitSound));
    }

    IEnumerator ShowHitMark(bool hitSound)
    {        
        yield return new WaitForSeconds(0.1f);
        if (!hitSound)
        {
            //AudioFW.PlayRandomPitch("HitMarker");
        }
        yield return new WaitForSeconds(0.1f);
    }
}
