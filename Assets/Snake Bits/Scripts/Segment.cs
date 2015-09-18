using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {

	int index;

	private bool hasEjectedProbe;

	void OnEnable () {
		hasEjectedProbe = false;
		GameObject head = GameObject.Find ("Head");

		if (index == 0) {
			transform.position = head.transform.position - head.transform.forward.normalized + transform.forward * -1.5f;
		} else {
			transform.position = head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward.normalized + transform.forward * -1.5f;
		}
	}

	void FixedUpdate () {
		GameObject head = GameObject.Find ("Head");

		if (index == 0) {
			transform.LookAt (head.transform.position - head.transform.forward);
			transform.Rotate (new Vector3 (0, 0, Mathf.LerpAngle (transform.rotation.eulerAngles.z, head.transform.rotation.eulerAngles.z, 0.99f)));
			/*
			if (Vector3.Distance (transform.position, head.transform.position) > 2.5f) {
				transform.position = Vector3.Lerp (transform.position, head.transform.position - head.transform.forward.normalized + transform.forward * -1.5f, 0.5f);
			}
			*/
			transform.position = head.transform.position - head.transform.forward.normalized + transform.forward * -1.5f;
		} else {
			transform.LookAt (head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward);
			transform.Rotate (new Vector3 (0, 0, Mathf.LerpAngle (transform.rotation.eulerAngles.z, head.GetComponent<Head> ().segments[index-1].transform.rotation.eulerAngles.z, 0.99f)));
			/*
			if (Vector3.Distance (transform.position, head.GetComponent<Head> ().segments[index-1].transform.position) > 2.5f) {
				transform.position = Vector3.Lerp (transform.position, head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward.normalized + transform.forward * -1.5f, 0.5f);
			}
			*/
			transform.position = head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward.normalized + transform.forward * -1.5f;
		}
	}

	void SetIndex (int ind) {
		index = ind;
	}
	public int getIndex () {
		return index;
	}
}