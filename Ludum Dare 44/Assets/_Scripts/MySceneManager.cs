using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MySceneManager : MonoBehaviour {
    [SerializeField]
    GameObject deathMenuToEnableLater;
    [SerializeField]
    GameObject gameObjectContainingTextMeshProButton;

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

        gameObjectContainingTextMeshProButton.GetComponent<Button>().Select();
    }

    public void ToExit() {
        Debug.Log("Navigating To Exit Game!");

        Application.Quit();
    }
}
