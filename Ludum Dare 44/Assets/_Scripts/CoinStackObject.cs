using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Stack", menuName = "Coin Stack", order = 51)]
public class CoinStackObject : ScriptableObject {

    [SerializeField]
    [Tooltip("Amount of money in stack")]
    private int coinAmount;

    [SerializeField]
    [Tooltip("Coin Stack Prefab")]
    private GameObject prefab;

}
