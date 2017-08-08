using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float negativeJumpForce;
    public float rollForce;
    public int rolldistance;
    

    Vector3 directionOfRoll;
    private Vector3 mousePosition;
    private Vector3 direction;
    private float distanceFromObject;
    private float movementSpeed;
    private bool m_grounded;


    //public Camera camera;

    void Start () {
        rb = GetComponent<Rigidbody>();
        m_grounded = true;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Movement(h, v);
        Turning();
        Jump();
        Dodge(h, v);

    }

    void Movement(float x, float v) {

       if( Input.GetKey(KeyCode.LeftShift)) {

            movementSpeed = runSpeed;
        }
        else { movementSpeed = walkSpeed; }
       
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
                rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
                directionOfRoll = transform.forward;
                break;

            case "backward":
                rb.MovePosition(transform.position - transform.forward * movementSpeed * Time.deltaTime);
                directionOfRoll = -transform.forward;
                break;

            case "right":
                rb.MovePosition(transform.position + transform.right * movementSpeed * Time.deltaTime);
                directionOfRoll = transform.right;
                break;

            case "left":
                rb.MovePosition(transform.position - transform.right * movementSpeed * Time.deltaTime);
                directionOfRoll = -transform.right;
                break;

            case "ForwardLeft":
                rb.MovePosition(transform.position - (transform.right - transform.forward).normalized * movementSpeed * Time.deltaTime);
                directionOfRoll = -(transform.right - transform.forward).normalized;
                break;

            case "ForwardRight":
                rb.MovePosition(transform.position + (transform.right + transform.forward).normalized * movementSpeed * Time.deltaTime);
                directionOfRoll = (transform.right + transform.forward).normalized;
                break;

            case "BackwardLeft":
                rb.MovePosition(transform.position - (transform.right + transform.forward).normalized * movementSpeed * Time.deltaTime);
                directionOfRoll = -(transform.right + transform.forward).normalized;
                break;

            case "BackwardRight":
                rb.MovePosition(transform.position + (transform.right - transform.forward).normalized * movementSpeed * Time.deltaTime);
                directionOfRoll = (transform.right - transform.forward).normalized;
                break; 
        }
       
    }

    void  Turning() {
        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && m_grounded == true) {

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            m_grounded = false;
        }
        if (m_grounded == false)
            rb.AddForce(-Vector3.up * negativeJumpForce, ForceMode.Impulse);
    }

    void Dodge(float h, float v) {

       
        if (h !=0 && Input.GetKeyDown(KeyCode.E) || v != 0 && Input.GetKeyDown(KeyCode.E)){
            print(Input.GetKeyDown(KeyCode.E));
            rb.AddForce(directionOfRoll * rollForce, ForceMode.Impulse - rolldistance);
        }
    }

    

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Map") {
            m_grounded = true;
        }
    }

}

