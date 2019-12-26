using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTextureBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<PlayerScript>().PlayerDead();
        else
            InGameEvents.CallDestroyBulletEvent();
    }
}
