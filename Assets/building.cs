using UnityEngine;
using System.Collections;

public class building : MonoBehaviour {

    void Awake () {
        for (int i = 1; i < 201; i++) {
            GameObject obj = transform.GetChild (i).gameObject;
            //obj.SetActive (false);
            obj.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    void OnCollisionEnter (Collision other) {
        /*
        if (other.gameObject.tag == "Snake") {
            Collider[] col = Physics.OverlapSphere (other.transform.position, 1, 1 << 10);
            for (int i = 0; i < col.Length; i++) {
                col[i].gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
            }
        }
        */
    }
}
