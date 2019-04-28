using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathMenuUpdater : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI textToChange;
    [SerializeField]
    GameObject gameObjectThatHasPlayerCurrency;

    void Update() {
        this.textToChange.text = $"Score: {gameObjectThatHasPlayerCurrency.GetComponent<PlayerCurrency>().GetHigestCurreny()}";
    }
}