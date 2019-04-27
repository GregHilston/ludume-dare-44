using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack_SingleThrow : MonoBehaviour {

    [SerializeField]
    private float cooldownTime;
    private float cooldownTimer = 0;

    [SerializeField]
    private float throwSpeed = 5000;

    [SerializeField]
    private Coin coinPrefab;
    [SerializeField]
    private ObjectPool coinPool;

    public UnityObjEvent onThrowCoin;

    private PlayerCurrency currency;

    void Start() {
        Initialize();
    }

    void Initialize() {
        currency = GetComponent<PlayerCurrency>();
    }

    void Update() {
        Attack();
    }

    void Attack() {
        if (attackReady()) {
            DoAttack();
        }
    }

    /// <summary>
    /// Decrements the timer if its value is greater than 0. Returns true if its value is 0, false if higher. Call this on every frame for accurate timer functionality.
    /// </summary>
    bool attackReady() {
        if (cooldownTimer > 0) {
            cooldownTimer -= Time.deltaTime;
            cooldownTimer = Mathf.Clamp(cooldownTimer,0,cooldownTimer);
        }
        return cooldownTimer <= 0;
    }

    void ResetTimer() {
        cooldownTimer = cooldownTime;
    }

    void DoAttack() {
        if (currency != null) {
            // Using KeyCode.E for testing purposes. Will replace this for better input button later.
            if (currency.GetCurrency() > 0 && Input.GetKeyDown(KeyCode.E)) {
                GameObject thrownCoinObject = coinPool.GetObjectFromPool(coinPrefab.gameObject,transform.position,transform.rotation);
                if (thrownCoinObject != null) {
                    Coin thrownCoin = thrownCoinObject.GetComponent<Coin>();
                    if (thrownCoin != null) {
                        thrownCoin.ShootCoin(throwSpeed);
                        onThrowCoin.Invoke(-1);
                    }
                    ResetTimer();
                }
            }
        } else  {
            Debug.LogError(gameObject.name + " must have a PlayerCurrency Component");
        }
    }

}
