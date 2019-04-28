using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour {
    [SerializeField]
    [Tooltip("The transform we'll rotate around slowly, for a nice cinematic look.")]
    private Transform transformToRotateAround;
    [SerializeField]
    [Tooltip("How quickly to rotate around the transform")]
    private float rotationSpeed = 3.0f;
    void Update() {
        this.transform.LookAt(this.transformToRotateAround);
        this.transform.Translate(Vector3.right * Time.deltaTime * this.rotationSpeed);
    }
}
