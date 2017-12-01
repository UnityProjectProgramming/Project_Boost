using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour {
    [SerializeField] Vector3 movementVector;
    [Range(0, 1)] [SerializeField] float movementFactor;
    Vector3 startingPos;
	// Use this for initialization
	void Start ()
    {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPos;
		
	}
}
