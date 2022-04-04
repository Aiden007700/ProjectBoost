using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;
    public float thrustForce;
    public float rotationForce;
    public AudioClip mainEngine;
    public ParticleSystem thrusterParticleSystem;
    public ParticleSystem lBoosterParticleSystem;
    public ParticleSystem rBoosterParticleSystem;

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
                thrusterParticleSystem.Play();
                audio.PlayOneShot(mainEngine);
            }
        } else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            audio.Stop();
            thrusterParticleSystem.Stop();
            lBoosterParticleSystem.Stop();
            rBoosterParticleSystem.Stop();
        }
        
    }

    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            rBoosterParticleSystem.Play();
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            lBoosterParticleSystem.Play();
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
