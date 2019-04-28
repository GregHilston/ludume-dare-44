using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour {

    private Rigidbody rb;

    private float throwSpeed;
    private bool throwCoin;
    public int coinDamage{get;}

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        ThrowTrajectory();
    }

    public void ShootCoin(float speed) {
        throwSpeed = speed;
        throwCoin = true;
    }

    void ThrowTrajectory() {
        if (throwCoin) {
            if (rb != null) {
                rb.velocity = (transform.forward * throwSpeed * Time.deltaTime);
            } else {
                Debug.LogError("No Rigidbody attached to " + gameObject.name);
            }
        }
    }

}
