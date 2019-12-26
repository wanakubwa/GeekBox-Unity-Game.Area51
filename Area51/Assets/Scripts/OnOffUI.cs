using UnityEngine;
using System.Collections;

public class OnOffUI : MonoBehaviour
{
    public void TurnOn()
    {
        InGameEvents.CallUIButtonPress();
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        InGameEvents.CallUIButtonPress();
        gameObject.SetActive(false);
    }
}
