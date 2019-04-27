using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private enum Playertypes{mainplayer,drone};

    [SerializeField]
    private Playertypes playerType;
    [SerializeField]
    private float moveSpeed = 10f;

    private RigidbodyConstraints rbConstraints;
    private Dictionary<Playertypes,string[]> playerInputAxes = new Dictionary<Playertypes,string[]>();

    void Start() {
        Initialize();
    }

    void Initialize() {
        rb = GetComponent<Rigidbody>();
        if (rb != null) {
            rbConstraints = rb.constraints;
        }
        playerInputAxes[Playertypes.mainplayer] = new string[2] {"Horizontal", "Vertical"};
        playerInputAxes[Playertypes.drone] = new string[2] {"Horizontal2", "Vertical2"};
    }

    void Update() {
        DoMovement();
    }

    void DoMovement() {
        Movement(playerInputAxes[playerType][0],playerInputAxes[playerType][1]);
    }

    void Movement(string hAxis, string vAxis) {
        if (rb != null) {
            float moveX = Input.GetAxis(hAxis);
            float moveZ = Input.GetAxis(vAxis);
            Vector3 moveDir = new Vector3(moveX,0,moveZ).normalized;
            
            if (moveDir != Vector3.zero) {
                rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
                rb.MoveRotation(Quaternion.Euler(rotationFromInput(moveX,moveZ)));
                rb.constraints = rbConstraints;
            } else {
                rb.velocity = Vector3.up * rb.velocity.y;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }  
        } else {
            Debug.LogError(gameObject.name + " does not have a Rigidbody enabled.");
        }
    }

    Vector3 rotationFromInput(float moveX, float moveZ) {
        return new Vector3( 0, Mathf.Atan2( moveX, moveZ) * 180 / Mathf.PI, 0 );
    }
}
