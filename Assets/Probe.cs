using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour {

	GameObject cam;

	public GameObject laser;

	public int index;

	void Awake () {
		cam = GameObject.Find ("CameraOrbit");
	}

	void FixedUpdate () {
		transform.LookAt (cam.GetComponent<OrbitCamera> ().point);
	}
	void Shoot () {
		laser.GetComponent<Laser> ().target = cam.GetComponent<OrbitCamera> ().point + new Vector3 (Random.value * 10, 0, Random.value * 10);
		Instantiate (laser, transform.position, Quaternion.identity);
	}
}
