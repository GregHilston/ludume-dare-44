using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_SingleThrow : MonoBehaviour {

    [SerializeField]
    private float cooldownTime;
    private float cooldownTimer = 0;

    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private ObjectPool coinPool;

    void Start() {

    }

    void Initialize() {

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
        // Using KeyCode.E for testing purposes. Will replace this for better input button later.
        if (Input.GetKeyDown(KeyCode.E)) {
            coinPool.GetObjectFromPool(coinPrefab,transform.position,transform.rotation,true,false);
            ResetTimer();
        }
    }

}
