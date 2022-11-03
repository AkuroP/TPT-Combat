using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_test : MonoBehaviour
{
    public VisualEffect hit01;
    public Transform hit01Pos;
    Transform playerTransform;

    void Start()
    {
        hit01Pos = hit01.GetComponent<Transform>();
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hit01Pos.position = playerTransform.position;
            hit01.Play();
        }
    }
}
