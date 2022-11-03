using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private readonly string volumeSFX = "Music_Volume";

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.SetFloat(volumeSFX, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float Parse (float value)
    {
        float parse = Mathf.Lerp(-80, 0, Mathf.Clamp01(value));
        return parse;
    }
}
