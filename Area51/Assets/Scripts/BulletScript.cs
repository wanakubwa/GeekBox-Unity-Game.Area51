using UnityEngine;
using System.Collections;
using System;
using ExplosionDataPattern;

public class BulletScript : MonoBehaviour
{
    [SerializeField] int bounceAvailable = 5;
    [SerializeField] float shakeDuration = 0.1f;
    [SerializeField] AudioClip bulletBounce;
    [SerializeField] float bulletBounceVolume = 0.3f;
    [SerializeField] AudioClip bulletExplosion;
    [SerializeField] float bulletExplosionVolume = 0.3f;

    int bounceCounter = 0;
    GameObject playerObject;
    ExplosionData explosionData;
    GameMenager gameMenager;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        explosionData = ExplosionData.getInstance();
        gameMenager = FindObjectOfType<GameMenager>();
        InGameEvents.destroyBulletEvent += DestroyBulletEvent_Handler;
    }

    private void OnDestroy()
    {
        InGameEvents.destroyBulletEvent -= DestroyBulletEvent_Handler;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(bulletBounce, Camera.main.transform.position, bulletBounceVolume);
        if (other.gameObject == playerObject && bounceCounter != 0)
        {
            gameMenager.CreateExplosionHitPlayerAnimAtPoint(transform.position);
            CreateExplosion(other);
            InGameEvents.CallDestroyBulletEvent();
        }
        else if(other.gameObject != playerObject)
        {
            gameMenager.CreateExplosionBounceAnimAtPoint(transform.position);
            bounceCounter++;
            if (bounceCounter >= bounceAvailable)
            {
                InGameEvents.CallDestroyBulletEvent();
            }
        }
    }

    private void DestroyBulletEvent_Handler()
    {
        var tmp = GetComponent<Animator>();
        tmp.SetBool("isEndStartAnim", false);
        gameObject.SetActive(false);
        bounceCounter = 0;
    }

    private void CreateExplosion(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(bulletExplosion, Camera.main.transform.position, bulletExplosionVolume);
        var rb = other.gameObject.GetComponent<Rigidbody2D>();
        var explosionPosition = gameObject.transform.position;

        StartCoroutine(CreateExplosionForce(rb, explosionPosition));
    }

    private IEnumerator CreateExplosionForce(Rigidbody2D rb, Vector3 explosionPosition)
    {
        Rigidbody2DExtension.AddExplosionForce(rb, explosionPosition, explosionData.ExplosionForce, explosionData.ExplosionBounceForce);
        yield return new WaitForSeconds(0f);
    }

    public void ExitAnimationState()
    {
        var tmp = GetComponent<Animator>();
        tmp.SetBool("isEndStartAnim", true);
    }
}