using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    float player;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    // Use this for initialization

    private Vector3 mousePosition;
    private Vector3 direction;
    private float distanceFromObject;
    private bool m_grounded;


    //public Camera camera;

    void Start () {
        rb = GetComponent<Rigidbody>();
        m_grounded = true;

    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        walking(h, v);
        turning();
        jump();
    }

    void walking(float x, float v) {

        string direction = "none";

        if (v > 0.1) direction = "forward";
        if (v < -0.1) direction = "backward";
        if (x > 0.1) direction = "right";
        if (x < -0.1) direction = "left";
        if (v > 0.1 && x < -0.1) direction = "ForwardLeft";
        if (v > 0.1 && x > 0.1) direction = "ForwardRight";
        if (v < -0.1 && x < -0.1) direction = "BackwardLeft";
        if (v < -0.1 && x > 0.1) direction = "BackwardRight";
        switch (direction) {
            case "forward":
                rb.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
                break;

            case "backward":
                rb.MovePosition(transform.position - transform.forward * walkSpeed * Time.deltaTime);
                break;

            case "right":
                rb.MovePosition(transform.position + transform.right * walkSpeed * Time.deltaTime);
                break;

            case "left":
                rb.MovePosition(transform.position - transform.right * walkSpeed * Time.deltaTime);
                break;

            case "ForwardLeft":
                rb.MovePosition(transform.position - (transform.right - transform.forward) * walkSpeed * Time.deltaTime);
                break;

            case "ForwardRight":
                rb.MovePosition(transform.position + (transform.right + transform.forward) * walkSpeed * Time.deltaTime);
                break;

            case "BackwardLeft":
                rb.MovePosition(transform.position - (transform.right + transform.forward) * walkSpeed * Time.deltaTime);
                break;

            case "BackwardRight":
                rb.MovePosition(transform.position + (transform.right - transform.forward) * walkSpeed * Time.deltaTime);
                break;
        }
    }

    void  turning() {
        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
    }

    void jump() {
        if (Input.GetButtonDown("Jump")) {
            m_grounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
        }
    }

}

