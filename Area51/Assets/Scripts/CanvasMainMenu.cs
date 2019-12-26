using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : MonoBehaviour
{
    private void Start()
    {
        var tmp = FindObjectOfType<Camera>();
        GetComponent<Canvas>().worldCamera = tmp;
    }

    public void EnergyButtonAction()
    {
        FindObjectOfType<GameMenager>().CreateADPopout();
    }
}
