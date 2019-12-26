using UnityEngine;
using System.Collections;

public class LaserHit : MonoBehaviour
{
    private LaserMainScript laserMainScript;

    private void Awake()
    {
        laserMainScript = transform.parent.GetComponent<LaserMainScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        laserMainScript.PlayerDead(other.gameObject);
    }

    private void EndAnimation()
    {
        laserMainScript.LaserHitAnimationEnd();
    }
}