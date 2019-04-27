using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool rotateWithTarget = false;

    [SerializeField]
    private Vector3 positionOffset;
    
    void Update() {
        DoFollowTarget();
    }

    void DoFollowTarget() {
        if (target != null) {
            transform.position = target.position + positionOffset;
            if (rotateWithTarget) {
                transform.rotation = target.rotation;
            }
        } else {
            Debug.LogError("No target for " + gameObject.name + " to follow.");
        }
    }
}
