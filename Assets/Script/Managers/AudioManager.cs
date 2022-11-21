using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    private readonly string musicVolume = "Music_Volume";

    // Start is called before the first frame update
    private void Start()
    {
        audioMixer.SetFloat(musicVolume, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static float ParseToDebit0(float value)
    {
        float parse = Mathf.Lerp(-80, 0, Mathf.Clamp01(value));
        return parse;
    }

    public static float ParseToDebit20(float value)
    {
        float parse = Mathf.Lerp(-80, 20, Mathf.Clamp01(value));
        return parse;
    }

    public static float ParseToDebit(float value, float min = -80, float max = 20)
    {
        float parse = Mathf.Lerp(min, max, Mathf.Clamp01(value));
        return parse;
    }
}
