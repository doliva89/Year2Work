using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    Transform temp;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float negativeJumpForce;
    public float rollForce;
    public float tapTime;
    public float slideStartAngel;
    public float slideMultiplyer;
    public float burm;
    public int rolldistance;

    Vector3 globalForce;
    Vector3 directionOfRoll;
    private Animator animator;

    private float movementSpeed;
    private float _doubleTapTimeA;
    private float _doubleTapTimeD;
    private float worldForwardAngle;
    private float worldRightAngle;

    private bool m_grounded;
    private bool doubleTapA = false;
    private bool doubleTapD = false;
    private bool movement = true;
    

    void Start() {
        globalForce = Vector3.zero;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        m_grounded = true;

    }

    // Update is called once per frame
    void FixedUpdate() {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        animator.SetFloat("_X", h);

        checkForDoubleTap(h,v);
        
           // Dodge(h, v);
            Movement(h, v);


        //  if (movement) {

        // }

      //  Turning();
        Jump();
        checkForSlopes();
        calculatingAngles();
        Landing();
        // print(rb.velocity.magnitude);

    }

    void Movement(float x, float v) {

        if (Input.GetKey(KeyCode.LeftShift)) {

            movementSpeed = runSpeed;
        }
        else { movementSpeed = walkSpeed; }

        float xProj = animator.GetFloat("xProj");
        Vector3 moveDir = transform.TransformDirection(xProj * burm, 0, v * movementSpeed);

        rb.MovePosition(transform.position + (moveDir * Time.deltaTime));
        rb.AddForce(moveDir*Time.deltaTime,ForceMode.VelocityChange);





        /*



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
        */
    }

    void checkForDoubleTap(float h, float v) {

         //doubleTapD = false;
        if (Input.GetKeyDown(KeyCode.A)) {
            if (Time.time < _doubleTapTimeA + tapTime) {
                doubleTapA = true;
                movement = false;
                Dodge(h, v);
            }
            _doubleTapTimeA = Time.time;
           
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            if (Time.time < _doubleTapTimeD + tapTime) {
                doubleTapD = true;
                movement = false;
                Dodge(h, v);
            }
            _doubleTapTimeD = Time.time;

        }

    }
    
    void Turning() {

        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
        //float dot = Vector3.Dot(transform.forward, globalForce);
        //print("global: " + globalForce);
        //print("DOT 1: "+dot);
        //dot = (1 - Mathf.Clamp01(dot + .3f));
        //print("DOT 2: " + dot);
        //transform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(transform.eulerAngles.y, Quaternion.LookRotation(globalForce).eulerAngles.y, ((Time.deltaTime * 1) * dot)), 0);
           
           
           
           //Vector3.Lerp(transform.rotation.eulerAngles, globalForce, ((Time.deltaTime * 1) * dot)));
        
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
        
        
            if (doubleTapA) {
            if (Input.GetKeyDown(KeyCode.A)) {
                if (rb.velocity.magnitude <= 0.5f && rb.velocity.magnitude >= -0.5f)
                rb.AddForce(directionOfRoll * rollForce, ForceMode.Impulse - rolldistance);
            }
        }

        if (doubleTapD) {
            if (Input.GetKeyDown(KeyCode.D)) {
                if (rb.velocity.magnitude <= 0.5f && rb.velocity.magnitude >= -0.5f)
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

        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
        float dot = Vector3.Dot(transform.forward, globalForce);
        dot = (1 - Mathf.Clamp01(dot + .1f));
        float yVal = Mathf.LerpAngle(transform.eulerAngles.y, Quaternion.LookRotation(globalForce).eulerAngles.y,((Time.deltaTime * 15) * dot));

        RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, out hit,5)) {

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, yVal, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }

    void calculatingAngles() {

        float forwardAngle = Vector3.Angle(transform.up, Vector3.forward);
        worldForwardAngle = forwardAngle - 90;
       

        float Rightangle = Vector3.Angle(transform.up, Vector3.right);
        worldRightAngle = Rightangle - 90;
       

        if (worldForwardAngle > slideStartAngel)
            globalForce.z = -(worldForwardAngle - slideStartAngel) * slideMultiplyer;

        else if (worldForwardAngle < -slideStartAngel)
            globalForce.z = Mathf.Abs((worldForwardAngle + slideStartAngel)* slideMultiplyer);

        else globalForce.z = 0;

        if (worldRightAngle > slideStartAngel)
            globalForce.x = -(worldRightAngle - slideStartAngel)* slideMultiplyer;
        else if (worldRightAngle < -slideStartAngel)
            globalForce.x = Mathf.Abs((worldRightAngle + slideStartAngel)* slideMultiplyer);
        else
            globalForce.x = 0;

        

      rb.AddForce(globalForce , ForceMode.Force);
    }

    void Landing() {

        RaycastHit hitForLanding;
        Vector3 down = transform.TransformDirection(Vector3.down);


        if (Physics.Raycast(transform.position, down, out hitForLanding)) {

            if (hitForLanding.distance <= 2f && hitForLanding.distance >= 1.2f && Input.GetMouseButtonDown(0)) {
               // print(hitForLanding.distance);
              //  print("landed");
                slideMultiplyer += .2f;

            }

            if (hitForLanding.distance < 1.2f && Input.GetMouseButtonDown(0) || hitForLanding.distance > 2f && Input.GetMouseButtonDown(0)) {
             //   print("Didnt land");
                slideMultiplyer = 1f;
            }
        }
    }
} 

