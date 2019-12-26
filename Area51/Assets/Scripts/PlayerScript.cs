using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject gunObject;
    [SerializeField] SpriteRenderer spriteRenderer;

    Rigidbody2D rigidbody2D;
    CapsuleCollider2D capsuleCollider2D;
    ScientistGun scientistGun;
    GameMenager gameMenager;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        scientistGun = gunObject.GetComponent<ScientistGun>();
        gameMenager = FindObjectOfType<GameMenager>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    public void EnteringEndLvlDoor()
    {
        capsuleCollider2D.enabled = false;
        rigidbody2D.velocity = new Vector2(0f, 0f);
        rigidbody2D.gravityScale = 0f;
        InGameEvents.CallPlayerWinLvlEvent();
        scientistGun.EnteringEndLvlDoor();
    }

    public void PlayerDead()
    {
        gameObject.SetActive(false);
        InGameEvents.CallPlayerDeadEvent();
        gameMenager.DeadPlayerBehaviour();
    }

    public void SetplayerComponentsColor(float value)
    {
        var tmp = spriteRenderer.color;
        tmp.a = value;
        spriteRenderer.color = tmp;
    }
}