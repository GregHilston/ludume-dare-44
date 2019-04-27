using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoinTrajectory : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float throwSpeed;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
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

}
