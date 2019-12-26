using UnityEngine;
using System.Collections;
using System;

public class EndLvlDoorMain : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 0.8f;
    [SerializeField] float moveStep = 1f;
    [SerializeField] Transform recallPoint;

    Animator animator;
    GameMenager gameMenager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameMenager = FindObjectOfType<GameMenager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<PlayerScript>().EnteringEndLvlDoor();
        StartCoroutine(MovePlayerToCenter(other.gameObject));
        StartCoroutine(DecreasePlayerOpacity(other.gameObject));
    }

    private IEnumerator DecreasePlayerOpacity(GameObject gameObject)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        var playerScript = gameObject.GetComponent<PlayerScript>();
        yield return new WaitForSeconds(0.2f);
        while (color.a > Mathf.Epsilon)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            playerScript.SetplayerComponentsColor(color.a);
            gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForEndOfFrame();
        }

        gameMenager.StageClearBehaviour();
    }

    private IEnumerator MovePlayerToCenter(GameObject gameObjects)
    {
        var tmp = 0f;
        while (Vector3.Distance(gameObjects.transform.position, transform.position) > Mathf.Epsilon)
        {
            tmp = moveStep * Time.deltaTime;
            gameObjects.transform.position = Vector3.MoveTowards(gameObjects.transform.position, recallPoint.position, tmp);
            yield return new WaitForEndOfFrame();
        }
    }

    private void EndDoorOpeningAnimation()
    {
        animator.SetBool("isEndOpeningAnim", true);
    }

    private void EndDoorClosingAnimation()
    {
        animator.SetBool("isEndClosingAnim", true);
    }

    public void PlayerNearbyAction()
    {
        animator.SetBool("isEndOpeningAnim", false);
        animator.SetBool("isPlayerNearby", true);
    }

    public void PlayerSteppedBack()
    {
        animator.SetBool("isEndClosingAnim", false);
        animator.SetBool("isPlayerNearby", false);
    }
}