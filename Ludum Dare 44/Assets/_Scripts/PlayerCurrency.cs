using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityObjEvent : UnityEvent<object> {

}

public class PlayerCurrency : MonoBehaviour {
    
    public UnityObjEvent OnCurrencyAmountChange;

    public UnityEvent OnPlayerDeath;

    private int currencyAmount;
    [SerializeField]
    private int startingCurrency = 100;

    private int highestCurreny;

    void Start() {
        this.highestCurreny = this.startingCurrency;
        ChangeCurrency(startingCurrency);
    }

    void Update() {
        if (Input.GetKey(KeyCode.V)) {
            ChangeCurrency(-1);
        }
        if (Input.GetKey(KeyCode.B)) {
            ChangeCurrency(1);
        }
    }

    public int GetCurrency() {
        return currencyAmount;
    }

    public int GetHigestCurreny() {
        return this.highestCurreny;
    }

    public void ChangeCurrency(int amount) {
        currencyAmount += amount;
        currencyAmount = Mathf.Clamp(currencyAmount, 0, currencyAmount);

        if (currencyAmount > this.highestCurreny) {
            this.highestCurreny = currencyAmount;
        }

        OnCurrencyAmountChange.Invoke(currencyAmount);
        if (currencyAmount <= 0) {
            OnPlayerDeath.Invoke();
        }
    }




}
