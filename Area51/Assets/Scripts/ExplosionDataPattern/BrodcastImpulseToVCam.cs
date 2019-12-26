using UnityEngine;
using System.Collections;
using Cinemachine;

public class BrodcastImpulseToVCam : MonoBehaviour
{
    private CinemachineImpulseSource m_ImpulseDefinition;

    private void Awake()
    {
        m_ImpulseDefinition = GetComponent<CinemachineImpulseSource>();
    }

    private void OnBecameVisible()
    {
        BrodcastImpulse();
    }

    public void BrodcastImpulse()
    {
        m_ImpulseDefinition.GenerateImpulse();
    }
}