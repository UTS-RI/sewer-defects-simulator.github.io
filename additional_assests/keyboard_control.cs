using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//as indicated by the rigidbody property changed by the key pressing, key D A S W controls the rotation and arrows controls the linear motion. 
//rotational and linear force can be applied at the same time
public class keyboard_control : MonoBehaviour
{
 
    
    public float rotationalMagnitude = 5f;//the magnitude of the force applied to rotate the object
    public float linearMagnitude = 5f;//the magnitude of the force applied to drive the linear movement of the object 
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Apply torque in the specified space and force mode
            rb.AddTorque(Vector3.up * rotationalMagnitude);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Apply torque in the specified space and force mode
            rb.AddTorque(Vector3.down * rotationalMagnitude);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // Apply torque in the specified space and force mode
            rb.AddTorque(Vector3.right * rotationalMagnitude);
        }
        if (Input.GetKey(KeyCode.W))
        {
            // Apply torque in the specified space and force mode
            rb.AddTorque(Vector3.left * rotationalMagnitude);
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            rb.AddForce(transform.forward * linearMagnitude);

        }
        if (Input.GetKey(KeyCode.DownArrow)){
            rb.AddForce(-transform.forward * linearMagnitude);

        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            rb.AddForce(-transform.right * linearMagnitude);

        }
        if (Input.GetKey(KeyCode.RightArrow)){
            rb.AddForce(transform.right * linearMagnitude);

        }
    }
}

/* code backup: 
float verticalInput = Input.GetAxis("Vertical"); // Get horizontal input from left/right arrow keys
        print(verticalInput);
        transform.Translate(new Vector3(0, 0, verticalInput * speed * Time.deltaTime)); // Move object based on input
 */