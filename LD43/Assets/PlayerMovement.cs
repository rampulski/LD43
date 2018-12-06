using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeedMax;
    public float walkSpeedMin;
    public float walkSpeedMid;
    public float walkSpeedCurrent;
    public float zPosThresholdFirst;
    public float zPosThresholdSecond;
    public float zPosMax;
    private FirstPersonController playerScript;

    public VideoPlayer projector;
    private float projectorAlphaCurrent;
    private float projectorAlphaNext;

    private Camera cam;
    public Transform statue;

    private GameManager gameManager;

    private float timeToEnd;

    public  AudioMixerSnapshot preEndSnapshot;
    public  AudioMixerSnapshot endSnapshot;

    private AudioSource endBlueSource;
    public AudioClip clip;


    void Start () {

        gameManager = FindObjectOfType<GameManager>();
        walkSpeedCurrent = walkSpeedMax;
        playerScript = GetComponent<FirstPersonController>();
        cam = Camera.main;
        endBlueSource = GameObject.FindWithTag("EndBlue").GetComponent<AudioSource>();

    }
	
	void Update () {


        if (transform.position.z > zPosThresholdFirst && transform.position.z < zPosThresholdSecond)
        {
            Debug.Log("First Threshold");
            walkSpeedCurrent = walkSpeedMid;
            projectorAlphaCurrent = projector.targetCameraAlpha;
            projectorAlphaNext = 0.1f;
            playerScript.CameraLocked = false;
            timeToEnd = 0f;
            if (!endBlueSource.isPlaying)
            {
                endBlueSource.PlayOneShot(clip);
            }
            preEndSnapshot.TransitionTo(6f);


        }
        else if (transform.position.z > zPosThresholdSecond)
        {
            Debug.Log("Second Threshold");

            walkSpeedCurrent = walkSpeedMin;
            projectorAlphaCurrent = projector.targetCameraAlpha;
            projectorAlphaNext = 0.55f;

            //lerp sur alpha
            playerScript.CameraLocked = true;
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, Quaternion.LookRotation(statue.transform.position - cam.transform.position), 0.5f);
            timeToEnd += Time.deltaTime;
            endSnapshot.TransitionTo(20f);

        }
        else
        {
            walkSpeedCurrent = walkSpeedMax;
            projectorAlphaCurrent = projector.targetCameraAlpha;
            projectorAlphaNext = 0f;

        }

        if (transform.position.z > zPosMax)
        {
            gameManager.End();
        }
        if (timeToEnd > 20f)
        {
            playerScript.CameraLocked = false;
            gameManager.EndBlue();
        }

        playerScript.m_WalkSpeed = walkSpeedCurrent;
        projector.targetCameraAlpha = Mathf.Lerp(projectorAlphaCurrent, projectorAlphaNext, 0.7f * Time.deltaTime);


    }

}
