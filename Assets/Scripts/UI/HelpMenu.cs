using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {

    public GameObject[] pages;
    GameObject activePage;

    public GameObject menu, help;

    void Start() {
        activePage = pages[0];
    }

    public void Back() {
        if (activePage == pages[0]) {
            menu.SetActive(true);
            help.SetActive(false);
        } else if (activePage == pages[1]) {
            SetActivePage(0);
        } else if (activePage == pages[2]) {
            SetActivePage(1);
        } else if (activePage == pages[3]) {
            SetActivePage(2);
        }
    }
    public void Next() {
        if (activePage == pages[0]) {
            SetActivePage(1);
        } else if (activePage == pages[1]) {
            SetActivePage(2);
        } else if (activePage == pages[2]) {
            SetActivePage(3);
        } else if (activePage == pages[3]) {
            return;
        }
    }

    void SetActivePage(int page) {
        for (int i = 0; i < pages.Length; i++) {
            if (i == page) {
                pages[i].SetActive(true);
                activePage = pages[i];
            } else {
                pages[i].SetActive(false);
            }
        }
    }
}
