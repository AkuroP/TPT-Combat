using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_test : MonoBehaviour
{
    public VisualEffect hit01;
    public Transform hit01Pos;
    Transform playerTransform;

    public float lerpAmountMax = 0.25f;
    public float lerpSpeed = 2f;
    float lerpAmount;

    Material mat;

    void Start()
    {
        hit01Pos = hit01.GetComponent<Transform>();
        playerTransform = GetComponent<Transform>();
        mat = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hit01Pos.position = playerTransform.position;
            hit01.Play();
            lerpAmount = lerpAmountMax;
        }

        if (lerpAmount > 0)
        {
            mat.SetFloat("_LerpAmount", lerpAmount);
            lerpAmount -= Time.deltaTime * lerpAmountMax * lerpSpeed;
        }
    }
}
