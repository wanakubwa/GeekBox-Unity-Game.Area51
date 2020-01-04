using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiperlink : MonoBehaviour
{
    [SerializeField] string facebookURL;

    public void FacebookButton()
    {
        InGameEvents.CallUIButtonPress();
        Application.OpenURL("https://www.facebook.com/GeekBox-109186153819085/");
    }
}