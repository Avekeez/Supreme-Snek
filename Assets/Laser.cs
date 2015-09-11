using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public Vector3 target;
	Vector3 t;

	float speed = 10;

	void Awake () {
		t = target;
		transform.LookAt (t);
	}

	void Update () {
		transform.position += transform.forward * speed;

		if (transform.position.y < 0) {
			Destroy (gameObject);
		}
	}
}
