using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour {

	Transform head;

	public GameObject mark;

	public Vector3 point;

	void Awake () {
		Cursor.visible = false;
		head = GameObject.Find ("Head").transform;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate () {
		transform.position = head.position + Vector3.up * 10;
		transform.Rotate (new Vector3 (-Input.GetAxis ("Mouse Y") * 5, Input.GetAxis ("Mouse X") * 5, -transform.rotation.eulerAngles.z));
		GameObject cam = transform.GetChild (0).gameObject;
		Debug.DrawRay (cam.transform.position, cam.transform.forward*1000, Color.green);

		int bitmask = 1 << 8;

		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, bitmask)) {
			point = hit.point;
			print ("Ayy");
		}
	}
}