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
        //Debug.Log("before apply zone : " + GameManager.instance.playerSavedStats.playerActualZone);
        //Debug.Log(GameManager.instance.allZone.Contains(nextZone));

        GameManager.instance.playerSavedStats.playerActualZone = GameManager.instance.allZone.FindIndex(x => x.Equals(nextZone));
        //Debug.Log("after apply zone : " + GameManager.instance.playerSavedStats.playerActualZone);
        
        actualZone.SetActive(false);
        nextZone.SetActive(true);
    }
}
