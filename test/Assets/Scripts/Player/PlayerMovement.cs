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
    float _doubleTapTimeD;
    public int rolldistance;


    Vector3 directionOfRoll;
    private Animator animator;
    private float movementSpeed;
    private bool m_grounded;
     public bool grounded;
    bool doubleTapD = false;
    bool movement = true;
    

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        m_grounded = true;

    }

    // Update is called once per frame
    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        checkForDoubleTap();
        Dodge(h, v);
        if (movement) {
            Movement(h, v);
        }
        Turning();
        Jump();
        
        checkForSlopes();
        
    }

    void Movement(float x, float v) {

        if (Input.GetKey(KeyCode.LeftShift)) {

            movementSpeed = runSpeed;
        }
        else { movementSpeed = walkSpeed; }

        string direction = "none";
        if (v == 0 && x == 0) direction = "none";
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
                // animator.SetBool("Running", true);
                break;

            case "backward":
                rb.MovePosition(transform.position - transform.forward * movementSpeed * Time.deltaTime);;
                //  animator.SetBool("Running", true);
                break;

            case "right":
                rb.MovePosition(transform.position + transform.right * movementSpeed * Time.deltaTime);
                directionOfRoll = transform.right;
                //animator.SetBool("Running", true);
                break;

            case "left":
                rb.MovePosition(transform.position - transform.right * movementSpeed * Time.deltaTime);
                directionOfRoll = -transform.right;
                // animator.SetBool("Running", true);
                break;

            case "ForwardLeft":
                rb.MovePosition(transform.position - (transform.right - transform.forward).normalized * movementSpeed * Time.deltaTime);
                //  animator.SetBool("Running", true);
                break;

            case "ForwardRight":
                rb.MovePosition(transform.position + (transform.right + transform.forward).normalized * movementSpeed * Time.deltaTime);
                // animator.SetBool("Running", true);
                break;

            case "BackwardLeft":
                rb.MovePosition(transform.position - (transform.right + transform.forward).normalized * movementSpeed * Time.deltaTime);
                ///animator.SetBool("Running", true);
                break;

            case "BackwardRight":
                rb.MovePosition(transform.position + (transform.right - transform.forward).normalized * movementSpeed * Time.deltaTime);
                //   animator.SetBool("Running", true);
                break;

            case "none":
                //   animator.SetBool("Running", false);
                break;
        }

    }

    void checkForDoubleTap() {

         //doubleTapD = false;
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)) {
            if (Time.time < _doubleTapTimeD + .3f) {
                doubleTapD = true;
                movement = false;
                
            }
            _doubleTapTimeD = Time.time;
           
        }
        if (doubleTapD) {
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) {
                print("space key was released");
                doubleTapD = false;

                movement = true;
                print(movement);
            }
            
        }
      
    }

    void Turning() {
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
        
        if (h != 0 && Input.GetKeyDown(KeyCode.A) || h != 0 && Input.GetKeyDown(KeyCode.D)) {
            if (doubleTapD) {
                rb.AddForce(directionOfRoll * rollForce, ForceMode.Impulse - rolldistance);
            }
        }
    }



    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Map") {
            m_grounded = true;
        }
    }

    void checkForSlopes() {

        RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, out hit)) {

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            Vector3 incomingVec = hit.point - transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawRay(hit.point, reflectVec, Color.green);
        

    }
      


    }
}

