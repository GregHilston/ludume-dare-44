using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetime : MonoBehaviour {

    [SerializeField]
    [Tooltip("The lifetime of the object. Set to 0 for indefinite lifetime.")]
    private float lifeTime = 0;
    private float lifeTimer;

    void Start() {
    }

    void OnEnable() {
        ResetTimer();
    }

    public void ResetTimer() {
        lifeTimer = lifeTime;
    }

    void Update() {
        HideAfterLifetime();
    }

    void HideAfterLifetime() {
        if (lifeTime != 0) {
            if (lifeTimer > 0) {
                lifeTimer -= Time.deltaTime;
            } else {
                HideItem();
            }
        }
    }

    void HideItem() {
        gameObject.SetActive(false);
    }
}
