using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PAMETERS - for tuning, typically set in the editor.
    //CACHE - e.g. references for readability or speed
    //STATE - private instance (member) variables
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100.0f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartingThrust();
        }
        else
        {
            StopThrust();
        }

        void StartingThrust()
        {
            rb.AddRelativeForce((Vector3.up * mainThrust * Time.deltaTime));          //(0,1,0) ya da Vector3(0,1,0) da yazabiliriz. 
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

            //Debug.Log("Pressed SPACE - Thrusting");
            if (!mainBooster.isPlaying)
            {
                mainBooster.Play();
            }
        }

        void StopThrust()
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            TurningLeft();
        }



        else if(Input.GetKey(KeyCode.D))
        {
            TurningRight();
        }

        else
        {
            TurningStop();
        }

        void TurningLeft()
        {
            ApplyRotation(-rotationThrust);   // (0,0,1) demek zaten   

            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
            //Debug.Log("Rotating Left");
        }

        void TurningRight()
        {
            ApplyRotation(rotationThrust);       //Vector 3'ün başına bir adet - koyup da bu işi çözebiliyormuşuz.!!! 

            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }                  //Ya da forward back koyabiliriz...
                               //Debug.Log("Rotating Right");
        }
    }

    private void TurningStop()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }


    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;        //freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);            //metot oluşturma sebebimiz yukarda 2 kez bunu kullanıyorduk ve
        rb.freezeRotation = false;  //unfreeze rotation so pyhsics system can take over.   //bir değişiklik olursa bunda yapınca ikisi de değişmiş olacak
    }     
    
                                                                                     //kodda sadelik ve okuyuculuk, hız kazanmak icin yaptık.
}                                                                               
