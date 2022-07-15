using System.Collections;
using System.Collections.Generic;
using AudioHelm;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static float beatPerSec;

    private AudioHelmClock helmClock;

    void Awake()
    {
        helmClock = GetComponent<AudioHelmClock>();
    }

    void Update()
    {
        beatPerSec = 60 / helmClock.bpm;
    }
}
