using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public Vector3 target;
	Vector3 t;

	float speed = 10;
    Rigidbody rb;

	void Awake () {
		t = target;
		transform.LookAt (t);
        rb = GetComponent<Rigidbody> ();
	}

	void Update () {
        rb.AddForce (transform.forward * speed, ForceMode.Impulse);
        if (Vector3.Distance (transform.position, Vector3.zero) > 500) {
            Destroy (gameObject);
        }
	}

    void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "Ground") {
            Destroy (gameObject);
        }
    }
}
