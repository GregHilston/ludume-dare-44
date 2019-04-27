using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetime : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 2f;
    private float lifeTimer;

    void Start() {
        ResetTimer();
    }

    public void ResetTimer() {
        lifeTimer = lifeTime;
    }

    void Update() {
        HideAfterLifetime();
    }

    void HideAfterLifetime() {
        if (lifeTimer > 0) {
            lifeTimer -= Time.deltaTime;
        } else {
            gameObject.SetActive(false);
        }
    }
}
