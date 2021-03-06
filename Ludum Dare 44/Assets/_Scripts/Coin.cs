﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour {

    private Rigidbody rb;

    private float throwSpeed;
    private bool throwCoin;
    private int coinDamage;

    public int CoinDamage{
        get {
            return coinDamage;
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        ThrowTrajectory();
    }

    public void ShootCoin(float speed, int damage = 1) {
        coinDamage = damage;
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

    void OnTriggerEnter(Collider col) {
        // If a coin hits a wall, the coin should be made no longer active
        MeshCollider mC = col.GetComponent<MeshCollider>();
        if (mC != null) {
            if (mC.gameObject.name.Contains("Cube")) {
                this.gameObject.SetActive(false);
            }
        }
    }
}
