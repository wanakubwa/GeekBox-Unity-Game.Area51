using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlock : MonoBehaviour
{
    [SerializeField] GameObject destroyParticle;

    Animator animator;
    Collider2D collider2D;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collider2D.enabled = false;
        animator.SetBool("isHitted", true);
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }

    public void SetObjectOff()
    {
        Destroy(gameObject);
    }
}