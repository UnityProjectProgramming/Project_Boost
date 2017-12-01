using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2.0f;

    float movementFactor;
    Vector3 startingPos;


	void Start ()
    {
        startingPos = transform.position;
	}

	void Update ()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f; 
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPos;	
	}
}
