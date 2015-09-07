using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public Vector3 target;
	public Transform Origin;
	Vector3 t;
	Transform o;

	float speed = 10;
	float startTime;
	float distance;

	void Awake () {
		t = target;
		//o = Origin;
		transform.LookAt (t);
		//startTime = Time.time;
		//distance = Vector3.Distance (o.transform.position, t);
		//transform.rotation = Quaternion.LookRotation (o.forward);
	}

	void Update () {
		//float f = ((Time.time - startTime) * speed) / distance;

		//transform.position = Vector3.Lerp (o.transform.position, t, f);
		transform.position += transform.forward * speed;

		if (transform.position.y < 0) {
			Destroy (gameObject);
		}

		//GetComponent<LineRenderer> ().SetPosition (0, Vector3.Lerp (o.transform.position, t, f));
		//GetComponent<LineRenderer> ().SetPosition (1, Vector3.Lerp (o.transform.position, t, f) + o.transform.forward*10);
	}
}
