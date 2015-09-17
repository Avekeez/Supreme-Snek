using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour {

	GameObject head;

	void Awake () {
		head = GameObject.Find ("Head");
	}

	void FixedUpdate () {
		int index = head.GetComponent<Head> ().currentSegments;
		transform.LookAt (head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward);
		transform.Rotate (new Vector3 (0, 0, Mathf.LerpAngle (transform.rotation.eulerAngles.z, head.GetComponent<Head> ().segments[index-1].transform.rotation.eulerAngles.z, 0.9f)));
		/*
			if (Vector3.Distance (transform.position, head.GetComponent<Head> ().segments[index-1].transform.position) > 2.5f) {
				transform.position = Vector3.Lerp (transform.position, head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward.normalized + transform.forward * -1.5f, 0.5f);
			}
			*/
		transform.position = head.GetComponent<Head> ().segments[index-1].transform.position - head.GetComponent<Head> ().segments[index-1].transform.forward.normalized + transform.forward * -0.8f;
	}
}
