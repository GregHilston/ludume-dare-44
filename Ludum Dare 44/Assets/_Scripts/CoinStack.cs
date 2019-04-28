using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinStack : MonoBehaviour
{

    public UnityEvent onPlayerGetCoins;   

    public void GivePlayerCurrency(int amount) {
        PlayerCurrency playerCurrency = FindObjectOfType<PlayerCurrency>();
        if (playerCurrency != null) {
            playerCurrency.ChangeCurrency(amount);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<PlayerMain>() != null) {
            onPlayerGetCoins.Invoke();
        }
    }

}
