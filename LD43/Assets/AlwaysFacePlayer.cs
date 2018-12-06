using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {

        transform.forward *= -1;

    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(Camera.main.transform);
		
	}
}
