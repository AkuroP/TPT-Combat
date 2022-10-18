using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3  posOffset;
    private Vector3 velocity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //met z en -10
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);

    }
}
