using UnityEngine;
using System.Collections;

public class building : MonoBehaviour {

    void Awake () {
        for (int i = 1; i < 201; i++) {
            transform.GetChild (i).gameObject.SetActive (false);
        }
    }
}
