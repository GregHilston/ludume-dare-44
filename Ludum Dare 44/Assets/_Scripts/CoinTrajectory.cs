using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoinTrajectory : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float throwSpeed;

    [SerializeField]
    private float lifeTime = 2f;
    private float lifeTimer;

    void Start() {
        rb = GetComponent<Rigidbody>();
        lifeTimer = lifeTime;
    }

    void Update() {
        HideAfterLifetime();
    }

    void FixedUpdate() {
        ThrowTrajectory();
    }

    void ThrowTrajectory() {
        if (rb != null) {
            rb.velocity = (transform.forward * throwSpeed * Time.deltaTime);
        } else {
            Debug.LogError("No Rigidbody attached to " + gameObject.name);
        }
    }

    void HideAfterLifetime() {
        if (lifeTimer > 0) {
            lifeTimer -= Time.deltaTime;
        } else {
            gameObject.SetActive(false);
        }
    }

}
