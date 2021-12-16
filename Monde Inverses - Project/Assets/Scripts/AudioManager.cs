using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup _soundEffectMixer;
    [SerializeField] AudioMixerGroup _musicMixer;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] List<AudioClip> _clips;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        PlayBackgroundMusic(_clips[0]);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        if (clip == null) return null;
        //Création d'un gameObject temporaire
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = _soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
