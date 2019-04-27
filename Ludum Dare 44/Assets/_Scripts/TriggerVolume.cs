using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour {

    [SerializeField]
    private Collider interactCollider;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;

    void OnTriggerEnter(Collider col) {
        if (col == interactCollider) {
            onTriggerEnter.Invoke();
        }
    }

    void OnTriggerStay(Collider col) {
        if (col == interactCollider) {
            onTriggerStay.Invoke();
        }
    }

    void OnTriggerExit(Collider col) {
        if (col == interactCollider) {
            onTriggerExit.Invoke();
        }
    }
}
