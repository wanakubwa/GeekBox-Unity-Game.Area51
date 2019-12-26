using UnityEngine;
using System.Collections;

public class LaserDetector : MonoBehaviour
{
    private LaserMainScript laserMainScript;

    private void Awake()
    {
        laserMainScript = transform.parent.GetComponent<LaserMainScript>();
    }

    private void EndAnimation()
    {
        laserMainScript.LaserStartedAnimationEnd();
    }
}