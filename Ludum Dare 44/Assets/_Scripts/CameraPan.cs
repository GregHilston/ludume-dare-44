using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowTarget))]
public class CameraPan : MonoBehaviour
{

    [SerializeField]
    private GameObject subject1;
    [SerializeField]
    private GameObject subject2;

    private FollowTarget followTarget;

    void Start() {
        followTarget = GetComponent<FollowTarget>();
    }

    void Update() {
        
    }
}
