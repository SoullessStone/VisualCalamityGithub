using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnY : MonoBehaviour {

	public float rotationSpeed = 100;

	private Transform trans;

	void Start(){
		trans = this.GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		trans.rotation = Vector3.up * -rotationSpeed * Time.deltaTime;
	}
}
