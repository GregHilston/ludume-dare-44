using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {
    [SerializeField]
    GameObject deathMenuToEnableLater;

    public void ToMainMenu() {
        Debug.Log("Navigating To Main Menu!");

        SceneManager.LoadScene("MainMenu");
    }

    public void ToGame() {
        Debug.Log("Navigating To Game!");

        SceneManager.LoadScene("Game");
    }

    public void ToDeath() {
        Debug.Log("Showing Death Menu");

        deathMenuToEnableLater.SetActive(true);
    }

    public void ToExit() {
        Debug.Log("Navigating To Exit Game!");

        Application.Quit();
    }
}
