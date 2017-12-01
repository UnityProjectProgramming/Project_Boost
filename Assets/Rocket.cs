﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] [Range(1.0f, 1000f)] float rcsThrust = 1;
    [SerializeField] [Range(1.0f, 1000f)] float mainThrust = 1;

    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip loadNextLevelSFX;
    [SerializeField] AudioClip crashSFX;

    [SerializeField] ParticleSystem mainEngineVFX;
    [SerializeField] ParticleSystem loadNextLevelVFX;
    [SerializeField] ParticleSystem crashVFX;


    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } // ignore Collisions when dead.

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                state = State.Alive;
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(loadNextLevelSFX);
        loadNextLevelVFX.Play();
        Invoke("LoadNextLevel", 1.0f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        mainEngineVFX.Stop();
        crashVFX.Play();
        Invoke("LoadFirstLevel", 1.0f);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); //TODO allow for more than 2 levels
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineVFX.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying && state == State.Alive)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        mainEngineVFX.Play();
    }

    private void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true;  // Take manual control of the rotation
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rcsThrust);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rcsThrust);
        }
        rigidbody.freezeRotation = false; // Resume Physics Control of rotation
    }

}
