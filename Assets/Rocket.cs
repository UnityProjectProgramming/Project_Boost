using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] [Range(1.0f, 1000f)] float rcsThrust = 1;
    [SerializeField] [Range(1.0f, 1000f)] float mainThrust = 1;

    Rigidbody rigidbody;
    AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Thrust();
        Rotate();
	}


    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                print("ok");
                SceneManager.LoadScene(1);
                break;
            default:
                print("Dead");
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }


    private void Rotate()
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
