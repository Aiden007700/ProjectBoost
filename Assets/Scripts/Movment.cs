using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;
    public float thrustForce;
    public float rotationForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        } else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            audio.Stop();
        }
    }

    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationForce);
        }
    }

    private void ApplyRotation(float rf)
    {
        rb.angularVelocity = Vector3.zero;
        transform.Rotate(Vector3.forward * rf * Time.deltaTime);
        rb.angularVelocity = Vector3.zero;
        //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
