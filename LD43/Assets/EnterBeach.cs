using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnterBeach : MonoBehaviour {

    private Transform player;

    public AudioMixerSnapshot beachSnapshot;
    public AudioMixerSnapshot caveSnapshot;


    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;


    }
	
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            beachSnapshot.TransitionTo(4f);
        }
    }
}
