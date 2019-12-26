using UnityEngine;
using System.Collections;

public class SceneTransitionAnimation : MonoBehaviour
{
    private void SetOff()
    {
        gameObject.SetActive(false);
        InGameEvents.CallStartCountingLvlTimeEvent();
    }
}