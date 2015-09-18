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

	private bool hasLeftGround;
	private bool allProbesEjected;

	void Awake () {
		allProbesEjected = false;
		currentSegments = 100;
		rb = GetComponent<Rigidbody> ();
		segments = new List<GameObject> (segments);
		for (int i = 0; i < maxSegments; i++) {
			GameObject obj = (GameObject) Instantiate (SegmentObj);
			obj.SendMessage ("SetIndex", i);
			segments.Add (obj);
			obj.SetActive (false);
		}
		probes = new List<GameObject> ();
		for (int i = 0; i < maxSegments; i++) {
			GameObject obj = (GameObject) Instantiate (ProbeObj);
			probes.Add (obj);
			obj.SetActive (false);
		}
		Instantiate (TailObj);
		rb.velocity = Vector3.up*-50;

        InvokeRepeating ("ejectProbe", 1, 1);
	}

	void FixedUpdate () {
		for (int i = 0; i < 100; i++) {
			segments[i].SetActive (true);
		} 
		if (transform.position.y < 0) {
			rb.AddForce (transform.up * -Input.GetAxis ("Pitch") * 250, ForceMode.Impulse);
			rb.useGravity = false;
		} else {
            rb.AddForce(transform.up * -Input.GetAxis("Pitch") * 50, ForceMode.Impulse);
            rb.useGravity = true;
		}
        //rb.AddForce (transform.right * Input.GetAxis ("Roll") * 100, ForceMode.Impulse);
        //transform.Rotate (transform.forward * -Input.GetAxis ("Roll") * 100);
        rb.AddTorque(transform.forward * -Input.GetAxis("Roll") * 10000);
        //Debug.Log (transform.forward * -Input.GetAxis("Roll") * 100);
        rb.velocity = rb.velocity.normalized * 50;
        Quaternion look = Quaternion.LookRotation(rb.velocity.normalized, transform.up);
        transform.rotation = look;
	}
	void Update () {
		for (int i = 0; i < 100 && !allProbesEjected; i ++) {
			if (!probes[i].activeInHierarchy) {
				allProbesEjected = false;
				break;
			}
			if (i == 99 && probes[99].activeInHierarchy) {
				allProbesEjected = true;
				break;
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			shoot ();
		}
		/*
		for (int i = 0; i < 100; i++) {
			for (int j = 0; j < 100; j++) {
				if (probes[i].activeInHierarchy && probes[j].activeInHierarchy && i != j) {
					if (probes[i].GetComponent<SphereCollider> ().bounds.Intersects (probes[j].GetComponent<SphereCollider> ().bounds)) {
						Vector3 direction = -(probes[i].transform.position-probes[j].transform.position).normalized;
						probes[i].transform.Translate (direction);
						probes[j].transform.Translate (-direction);
					}
				}
			}
		}
		*/
	}

	void shoot () {
		for (int i = 0; i < 100; i++) {
			if (probes[i].activeInHierarchy) {
				probes[i].SendMessage ("Shoot");
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Ground" && rb.velocity.y > 40) {
			rb.AddForce (Vector3.up*100000);
		}
	}

    void OnCollisionEnter (Collision other) {
        Debug.Log ("ayy");
        if (other.gameObject.layer == 12) {
            GameObject p = other.gameObject.transform.parent.gameObject;
            for (int i = 1; i < p.transform.childCount; i++) {
                GameObject c = p.transform.GetChild (i).gameObject;
                c.SetActive (true);
                c.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
                //c.GetComponent<Rigidbody> ().AddExplosionForce (10, transform.position, 5, 0, ForceMode.Impulse);
            }
            //other.gameObject.SetActive (false);
        }
    }

    void ejectProbe () {
        if (!allProbesEjected) {
            for (int i = 0; i < 100; i++) {
                if (Random.value < 0.05f && !probes[i].activeInHierarchy)
                {
                    StartCoroutine(ejectProbe_(i));
                }
            }
        }
    }

	IEnumerator ejectProbe_ (int i) {
		Transform segment = segments [i].transform;
		Transform probe = probes[i].transform;
		if (segment.position.y < 20) {
			Vector3 c = segment.position;
			c.y = 0;
			probes[i].GetComponent<Probe> ().target = new Vector3 (segment.position.x, 10, segment.position.z) 
					+ new Vector3 ((Random.value-0.5f)*100, 20+Random.value*50, (Random.value-0.5f)*100);
			probe.position = c - Vector3.up;
			segment.GetChild (1).gameObject.SetActive (false);
			probes[i].SetActive (true);
		} else {
            Vector3 target = segment.position
                    + segment.up * (10 + Random.value * 10)
                    + segment.right * (Random.value - 0.5f) * 100
                    + segment.forward * (Random.value - 0.5f) * 100;
            if (target.y < 0) {
                target.y = Random.value * 50;
            }
            probes[i].GetComponent<Probe>().target = target;
            probe.position = segment.position + segment.up;
			segment.GetChild (1).gameObject.SetActive (false);
			probes[i].SetActive (true);
		}
		yield return new WaitForSeconds (0.5f);
	}
}
