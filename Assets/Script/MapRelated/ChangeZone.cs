using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeZone : MonoBehaviour
{

    public GameObject actualZone;
    public GameObject nextZone;

    public void GoToZone()
    {
        if(actualZone == null || nextZone == null)return;
        CinemachineConfiner2D newConfiner = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineConfiner2D>();
        PolygonCollider2D collForConfiner = nextZone.GetComponentInChildren<PolygonCollider2D>();
        newConfiner.m_BoundingShape2D = collForConfiner;
        actualZone.SetActive(false);
        nextZone.SetActive(true);
    }
}
