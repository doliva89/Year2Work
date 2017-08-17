using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

  
    public Transform cube;
    float t;
	// Use this for initialization
	void Start () {
      
       
       
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //float mouseInput = Input.GetAxis("Mouse X");
        //Vector3 lookhere = new Vector3(0, mouseInput, 0);
        //transform.Rotate(lookhere);

        if (Input.GetKey(KeyCode.Mouse1)) {
            
            float mouseInputy = Input.GetAxis("Mouse Y");
            Vector3 lookUpDown = new Vector3(mouseInputy, 0, 0);
            transform.Rotate(lookUpDown);

          
        }
        else {

           // t += .1f * Time.deltaTime;
           // transform.rotation = Quaternion.Euler(Mathf.Lerp( 30, transform.rotation.eulerAngles.x,t), cube.transform.rotation.eulerAngles.y, cube.transform.rotation.eulerAngles.z);
           //t = 0;
            
        }
    }
}
