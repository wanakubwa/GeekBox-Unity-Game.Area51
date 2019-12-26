using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class LaserMainScript : MonoBehaviour
{
    [SerializeField] GameObject laserDetector;
    [SerializeField] GameObject laserMainHit;
    [SerializeField] float delay = 5f;

    float timeCounter = 0f;
    bool isLaserEnabled = false;
    bool isLaserAnimating = false;

    private void Start()
    {
        laserDetector.SetActive(false);
        laserMainHit.SetActive(false);
    }

    private void Update()
    {
        if (!isLaserEnabled && IsTimeAchived())
        {
            isLaserEnabled = true;
            laserDetector.SetActive(true);
        }
    }

    private bool IsTimeAchived()
    {
        if(timeCounter >= delay)
        {
            timeCounter = 0f;
            return true;
        }

        timeCounter += Time.deltaTime;
        return false;
    }

    public void LaserStartedAnimationEnd()
    {
        laserDetector.SetActive(false);
        laserMainHit.SetActive(true);
    }

    public void LaserHitAnimationEnd()
    {
        laserMainHit.SetActive(false);
        isLaserEnabled = false;
    }

    public void PlayerDead(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerScript>().PlayerDead();
    }
}