using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

    Quaternion startPos;
    Quaternion newPos;
    public Transform cube;
    float t;
	// Use this for initialization
	void Start () {
        startPos.x = transform.rotation.eulerAngles.x;
       
       
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.Mouse1)) {
            
            float mouseInputy = Input.GetAxis("Mouse Y");
            Vector3 lookUpDown = new Vector3(mouseInputy, 0, 0);
            transform.Rotate(lookUpDown);

            newPos.x = transform.rotation.x;
            newPos.w = transform.rotation.w;
        }
        else {

           // t += .1f * Time.deltaTime;
           // transform.rotation = Quaternion.Euler(Mathf.Lerp( 30, transform.rotation.eulerAngles.x,t), cube.transform.rotation.eulerAngles.y, cube.transform.rotation.eulerAngles.z);
           //t = 0;
            
        }
    }
}
