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
		transform.position = head.position + Vector3.up * 5;
		transform.Rotate (new Vector3 (-Input.GetAxis ("Mouse Y") * 5, Input.GetAxis ("Mouse X") * 5, -transform.rotation.eulerAngles.z));
	}

	void Update () {
		GameObject cam = transform.GetChild (0).gameObject;
		Debug.DrawRay (cam.transform.position, cam.transform.forward*1000, Color.green);
		RaycastHit hit;
		if ((Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, 1 << 8 + 1 << 12) || Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, 1 << 12)) && Input.GetMouseButtonDown (0)) {
			point = hit.point;
			mark.transform.position = point;
		}
	}
}