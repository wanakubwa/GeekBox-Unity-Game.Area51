using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] float explosionForce = 5f;
    [SerializeField] float explosionBounceForce = 2f;
    [SerializeField] AudioClip mineExplosionSound;
    [SerializeField] float mineExplosionSoundVolume = 0.3f;

    BrodcastImpulseToVCam brodcastImpulseToVCam;
    GameMenager gameMenager;

    private void Awake()
    {
        gameMenager = FindObjectOfType<GameMenager>();
        brodcastImpulseToVCam = GetComponent<BrodcastImpulseToVCam>();
    }

    private IEnumerator DestroyAnimation()
    {
        gameMenager.CreateExplosionMineAnimAtPoint(transform);
        brodcastImpulseToVCam.BrodcastImpulse();
        yield return null; 
    }

    public void DestroyMine()
    {
        StartCoroutine(DestroyAnimation());
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(mineExplosionSound, Camera.main.transform.position, mineExplosionSoundVolume);

        var bd2D = other.GetComponent<Rigidbody2D>();

        StartCoroutine(DestroyAnimation());
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerScript>().PlayerDead();
            Rigidbody2DExtension.AddExplosionForce(bd2D, transform.position, explosionForce, explosionBounceForce);
        }
        Destroy(gameObject);
    }
}