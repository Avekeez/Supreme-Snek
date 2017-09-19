using UnityEngine;
using System.Collections;

public class building : MonoBehaviour {

    void Awake () {
        transform.GetChild (0).gameObject.SetActive (false);
        for (int i = 0; i < transform.childCount; i++) {
            GameObject obj = transform.GetChild (i).gameObject;
            //obj.SetActive (false);
            obj.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
