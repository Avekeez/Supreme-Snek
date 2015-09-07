using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Head : MonoBehaviour {

	Rigidbody rb;

	public int maxSegments;

	public List<GameObject> segments;
	public List<GameObject> probes;
	public GameObject SegmentObj;
	public GameObject ProbeObj;
	public GameObject TailObj;

	public int currentSegments;

	private bool alive;

	private bool hasLeftGround;

	private int probesEjected;

	void Awake () {
		alive = true;
		currentSegments = 100;
		rb = GetComponent<Rigidbody> ();
		segments = new List<GameObject> (segments);
		for (int i = 0; i < maxSegments; i++) {
			GameObject obj = (GameObject) Instantiate (SegmentObj);
			obj.SendMessage ("SetIndex", i);
			segments.Add (obj);
		}
		probes = new List<GameObject> ();
		for (int i = 0; i < maxSegments; i++) {
			GameObject obj = (GameObject) Instantiate (ProbeObj);
			probes.Add (obj);
			obj.SetActive (false);
		}
		Instantiate (TailObj);
		rb.velocity = Vector3.forward*50;
		probesEjected = 0;
	}

	void FixedUpdate () {
		if (transform.position.y < 0) {
			rb.AddForce (transform.up * -Input.GetAxis ("Pitch") * 250, ForceMode.Impulse);
			rb.useGravity = false;
		} else {
			rb.useGravity = true;
		}
		rb.AddForce (transform.right * Input.GetAxis ("Roll") * 100, ForceMode.Impulse);
		rb.velocity = rb.velocity.normalized * 50;
		transform.rotation = Quaternion.LookRotation (rb.velocity.normalized);
	}
	void Update () {
		if (Input.GetMouseButtonDown (1) && probesEjected < 100) {
			if (segments[probesEjected].transform.position.y > 5 && segments[probesEjected].transform.position.y < 50) {
				probes[probesEjected].transform.position = segments[probesEjected].transform.position + segments[probesEjected].transform.up * 5;
				segments[probesEjected].transform.GetChild (1).gameObject.SetActive (false);
				probes[probesEjected].SetActive (true);
				probesEjected ++;
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			StartCoroutine (shoot ());
		}
	}

	IEnumerator shoot () {
		for (int i = 0; i < probesEjected; i++) {
			probes[i].SendMessage ("Shoot");
			yield return new WaitForSeconds (0.01f);
		}
	}
}
