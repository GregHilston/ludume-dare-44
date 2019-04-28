using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour {

    private delegate void action();
    private Dictionary<string, action> playerAttacks = new Dictionary<string, action>();

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

    [SerializeField]
    private int numShotsInMulti;
    [SerializeField]
    private float multiShotRange = 45f;

    void Start() {
        Initialize();
    }

    void Initialize() {
        currency = GetComponent<PlayerCurrency>();
        playerAttacks.Add("Custom1",DoOneShot);
        playerAttacks.Add("Custom2",DoMultiShot);
    }

    void Update() {
        Attack();
    }

    void Attack() {
        if (attackReady()) {
            foreach(string input in playerAttacks.Keys) {
                if (Input.GetButtonDown(input)) {
                    playerAttacks[input]();
                }
            }
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

    void DoOneShot() {
        OneShot(transform.rotation);
    }

    void DoMultiShot() {
        float startingAngle = transform.rotation.eulerAngles.y - ( multiShotRange / 2);
        int numShots = numShotsInMulti;
        if (numShotsInMulti > currency.GetCurrency()) {
            numShots = currency.GetCurrency();
        }
        float angleBetweenShots = multiShotRange / (numShots - 1);

        for (int i = 0; i < numShots; i++) {
            OneShot(Quaternion.Euler(Vector3.up * (startingAngle + (angleBetweenShots * i))));
        }

    }

    void OneShot(Quaternion rotation) {
        if (currency != null) {
            // Using KeyCode.E for testing purposes. Will replace this for better input button later.
            if (currency.GetCurrency() > 0) {
                GameObject thrownCoinObject = coinPool.GetObjectFromPool(coinPrefab.gameObject,transform.position,rotation);
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
