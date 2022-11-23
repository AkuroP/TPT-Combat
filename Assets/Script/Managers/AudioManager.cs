using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixerGroup soundEffectMixer;
    public AudioMixerGroup ostMixer;

    [System.Serializable]
    public class KeyValue
    {
        public string audioName;
        public AudioClip audio;
    }
    public List<KeyValue> audioLibrary = new List<KeyValue>();
    public Dictionary<string, AudioClip> allAudio = new Dictionary<string, AudioClip>();

    private GameObject actualOST;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        foreach(var ui in audioLibrary)
        {
            allAudio[ui.audioName] = ui.audio;
        }
        
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos, AudioMixerGroup whatMixer, bool isSFX)
    {
        //Create GameObject
        GameObject tempGO = new GameObject("TempAudio");
        //pos of GO
        tempGO.transform.position = pos;
        //Add an audiosource
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        if(isSFX)
        {
            float randomPitch = Random.Range(-5, 6);
            audioSource.pitch += (randomPitch / 60);
        }
        //Get the audio mixer
        audioSource.outputAudioMixerGroup = whatMixer;
        audioSource.Play();
        //Destroy at the lenght of the clip
        if(whatMixer != ostMixer)Destroy(tempGO, clip.length);
        else
        {
            audioSource.loop = true;
            actualOST = tempGO;
        } 
        return audioSource;
    }

    public void DestroyOST()
    {
        if(actualOST == null)return;
        Destroy(actualOST);
    }
}
