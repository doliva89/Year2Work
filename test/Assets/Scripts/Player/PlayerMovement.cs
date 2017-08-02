using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    float player;
    public float walkSpeed;
    public float runSpeed;
    // Use this for initialization

    private Vector3 mousePosition;
    private Vector3 direction;
    private float distanceFromObject;
    public Camera camera;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.z, 10);
        //Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        //lookPos = lookPos - transform.position;
        //float angle = Mathf.Atan2(lookPos.z, lookPos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);

        walking(h, v);
        turning();
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

}

