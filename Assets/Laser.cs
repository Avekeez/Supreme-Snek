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
        if (transform.position.y < 0) {
            Destroy (gameObject);
        }
	}

    void OnCollisionEnter (Collision other) {
        if (other.gameObject.layer == 12) {
            GameObject p = other.gameObject.transform.parent.gameObject;
            for (int i = 1; i < p.transform.childCount; i++) {
                GameObject c = p.transform.GetChild (i).gameObject;
                if (Vector3.Distance (transform.position, c.transform.position) < 10) {
                    c.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
                    Vector3 force = -transform.forward*100;
                    force.y = 0;
                    c.GetComponent<Rigidbody> ().AddForce (force);
                    //c.SetActive (false);
                }
                //c.GetComponent<Rigidbody> ().AddExplosionForce (10, transform.position, 5, 0, ForceMode.Impulse);
            }
            //other.gameObject.SetActive (false);
        }
        Destroy (gameObject);
    }
}
