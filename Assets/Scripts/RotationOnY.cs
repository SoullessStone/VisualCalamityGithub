using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnY : MonoBehaviour {

	public float rotationSpeed;

	private Transform trans;

	void Start(){
		trans = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		trans.Rotate (Vector3.up * -rotationSpeed * Time.deltaTime);
	}
}
