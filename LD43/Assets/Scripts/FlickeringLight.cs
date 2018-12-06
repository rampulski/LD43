﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public float MaxReduction;
    public float MaxIncrease;
    public float RateDamping;
    public float Strength;
    public bool StopFlickering;

    private Light _lightSource;
    private float _baseIntensity;
    private bool _flickering;

    public float startIntensity;
    public float targetIntensity;
    public float maxStrength;

    public void Reset()
    {
        MaxReduction = 0.2f;
        MaxIncrease = 0.2f;
        RateDamping = 0.1f;
        Strength = 300;
    }

    public void Start()
    {
        _lightSource = GetComponent<Light>();
        if (_lightSource == null)
        {
            Debug.LogError("Flicker script must have a Light Component on the same GameObject.");
            return;
        }
        StopFlickering = false;
        _baseIntensity = _lightSource.intensity;
        StartCoroutine(DoFlicker());

    }

    void Update()
    {
        if (!StopFlickering && !_flickering)
        {
            //StartCoroutine(DoFlicker());
        }
    }

    private IEnumerator DoFlicker()
    {

        _flickering = true;
        while (!StopFlickering)
        {
            _lightSource.intensity = Mathf.Lerp(startIntensity, targetIntensity, Strength / maxStrength);

            Strength += 10 * Time.deltaTime;
            if (Strength > maxStrength)
            {
                Strength = 0;
                startIntensity = _lightSource.intensity;
                targetIntensity = Random.Range(_baseIntensity - MaxReduction, _baseIntensity + MaxIncrease);
            }
            yield return new WaitForSeconds(RateDamping);
        }
        _flickering = false;
    }
}