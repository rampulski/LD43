using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlaybackSpeedVariation : MonoBehaviour {

    public float MaxReduction;
    public float MaxIncrease;
    public float RateDamping;
    public float Strength;
    public bool StopFlickering;

    private float _baseSpeed;
    private bool _flickering;

    public float startSpeed;
    public float targetSpeed;
    public float maxStrength;

    private VideoPlayer vP;

    void Start () {

       //MaxReduction = 0.2f;
       //MaxIncrease = 0.2f;
       //RateDamping = 0.1f;
        Strength = 0;

        vP = GetComponent<VideoPlayer>();
        _baseSpeed = vP.playbackSpeed;
        StartCoroutine(DoFlicker());

    }

    // Update is called once per frame
    void Update () {
		
	}

    private IEnumerator DoFlicker()
    {

        _flickering = true;
        while (!StopFlickering)
        {
            vP.playbackSpeed = Mathf.Lerp(startSpeed, targetSpeed, Strength / maxStrength);

            Strength += 500 * Time.deltaTime;
            if (Strength > maxStrength)
            {
                Strength = 0;
                startSpeed = vP.playbackSpeed;
                targetSpeed = Random.Range(_baseSpeed - MaxReduction, _baseSpeed + MaxIncrease);
            }
            yield return new WaitForSeconds(RateDamping);
        }
        _flickering = false;
    }

}
