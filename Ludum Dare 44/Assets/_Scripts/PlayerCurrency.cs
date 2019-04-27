using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityObjEvent : UnityEvent<object> {

}

public class PlayerCurrency : MonoBehaviour {
    
    public UnityObjEvent OnCurrencyAmountChange;

    private int currencyAmount;
    [SerializeField]
    private int startingCurrency = 100;

    void Start() {
        ChangeCurrency(startingCurrency);
    }

    public int GetCurrency() {
        return currencyAmount;
    }

    public void ChangeCurrency(int amount) {
        currencyAmount += amount;
        currencyAmount = Mathf.Clamp(currencyAmount,0,currencyAmount);
        OnCurrencyAmountChange.Invoke(currencyAmount);
    }


}
