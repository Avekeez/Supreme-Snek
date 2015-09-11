using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour {

	GameObject cam;

	public GameObject laser;

	public int index;

	public bool isActive = false;

	public Vector3 target;

	float startTime;
	float distance;
	Vector3 vInitial;

	void OnEnable () {
		cam = GameObject.Find ("CameraOrbit");
		startTime = Time.time;
		distance = Vector3.Distance (transform.position, target);
		vInitial = transform.position;
		InvokeRepeating ("Shoot", distance / 10, 3);
	}

	void FixedUpdate () {
		transform.LookAt (cam.GetComponent<OrbitCamera> ().point);
	}
	void Update () {
		float f = 10*(Time.time - startTime) / distance;
		transform.position = Vector3.Lerp (vInitial, target, f);
		if (transform.position == target) {
			isActive = true;
		}
	}
	void Shoot () {
		if (isActive) {
			Invoke ("Shoot_", Random.value*2);
		}
	}
	void Shoot_ () {
		laser.GetComponent<Laser> ().target = cam.GetComponent<OrbitCamera> ().point + new Vector3 ((Random.value - 0.5f) * 10, 0, (Random.value - 0.5f) * 10);
		Instantiate (laser, transform.position, Quaternion.identity);
	}
}
