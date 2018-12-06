using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.Characters.FirstPerson;

public class AttractPlayer : MonoBehaviour {

    public Transform player;
    public float maxDistance;

    public RaycastHit raycastHit;

    private FirstPersonController playerScript;
    private GameManager gM;

    public bool attracted;
    public bool haveBeenAttracted;
    public bool attractionHasStopped;
    private bool canResetRot;

    private float timeToEnd;

    private AudioSource succubeSource;

    private RaycastHit hit;


    void Start () {

        playerScript = FindObjectOfType<FirstPersonController>();
        gM = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        succubeSource = GetComponent<AudioSource>();
        attracted = false;
        haveBeenAttracted = false;
    }
	
	void Update () {


        if (Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            Debug.Log("In Distance");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 50f)) 
           {

                Debug.Log(raycastHit.collider);

                if (raycastHit.collider.CompareTag("Succube"))
               {
                    Debug.Log("RAYCASTED");
                    attracted = true;
                    haveBeenAttracted = true;
               }
           }
        }
        else
        {
            attracted = false;
        }


        if (attracted)
        {
            timeToEnd += Time.deltaTime;
            canResetRot = true;
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.LookRotation(transform.position - Camera.main.transform.position), 0.5f);
            player.rotation = Quaternion.RotateTowards(player.rotation, Quaternion.LookRotation(transform.position - player.position), 0.5f);
            if (!succubeSource.isPlaying)
            {
                succubeSource.Play();

            }
            if (haveBeenAttracted)
            {
                playerScript.CameraLocked = true;
                attractionHasStopped = true;

                haveBeenAttracted = false;
            }
        }
        else
        {
            succubeSource.Stop();
            timeToEnd = 0f;

            // if (canResetRot)
            // {
            // timeToEnd = 0f;
            //
            // Vector3 charDir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            // Quaternion quat = Quaternion.LookRotation(charDir);
            //
            // playerScript.GetMouseLook().ResetRotation(quat, Camera.main.transform.rotation);
            if (attractionHasStopped)
            {
                playerScript.CameraLocked = false;
                attractionHasStopped = false;

            }
            //    }
            //
            //  canResetRot = false;
        }

        if (timeToEnd > 14.5f)
        {
            gM.EndRed();
            timeToEnd = 0f;
        }

    }


}
