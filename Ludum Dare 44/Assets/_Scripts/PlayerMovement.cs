using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    public enum Playertypes{mainplayer,drone};
    public Playertypes playerType;
    public float moveSpeed = 10f;
    private RigidbodyConstraints rbConstraints;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbConstraints = rb.constraints;

    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }

    void DoMovement() {
        switch(playerType) {
            case Playertypes.mainplayer:
                Movement("Horizontal","Vertical");
            break;
            case Playertypes.drone:
                Movement("Horizontal2","Vertical2");
            break;
        }
    }

    void Movement(string hAxis, string vAxis) {
        float moveX = Input.GetAxis(hAxis);
        float moveZ = Input.GetAxis(vAxis);
        Vector3 moveDir = new Vector3(moveX,0,moveZ).normalized;
        
        if (moveDir != Vector3.zero) {
            Debug.Log("THERE IS INPUT");
            rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
            rb.MoveRotation(Quaternion.Euler(new Vector3( 0, Mathf.Atan2( moveX, moveZ) * 180 / Mathf.PI, 0 )));
            rb.constraints = rbConstraints;
        } else {
            Debug.Log("NO INPUT");
            rb.velocity = Vector3.up * rb.velocity.y;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
