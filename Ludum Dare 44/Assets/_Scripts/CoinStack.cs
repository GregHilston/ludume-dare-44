using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStack : MonoBehaviour
{
    
    public void GivePlayerCurrency(int amount) {
        PlayerCurrency playerCurrency = FindObjectOfType<PlayerCurrency>();
        if (playerCurrency != null) {
            playerCurrency.ChangeCurrency(amount);
        }
    }

}
