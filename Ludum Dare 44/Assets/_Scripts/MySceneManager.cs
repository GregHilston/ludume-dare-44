using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {
    public void ToMainMenu() {
        Debug.Log("Navigating To Main Menu!");

        SceneManager.LoadScene("MainMenu");
    }

    public void ToGame() {
        Debug.Log("Navigating To Game!");

        SceneManager.LoadScene("Game");
    }

    public void ToDeath() {
        SceneManager.LoadScene("DeathMenu");
    }

    public void ToExit() {
        Debug.Log("Navigating To Exit Game!");

        Application.Quit();
    }
}
