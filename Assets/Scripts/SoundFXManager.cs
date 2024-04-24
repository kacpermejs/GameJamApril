using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
  public static SoundFXManager instance;

  [SerializeField] private AudioSource soundEffectObject;

  private void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  public void PlayAudioClip(AudioClip audioClip, Transform spawnTransform, float volume) {
    AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

    audioSource.clip = audioClip;
    audioSource.volume = volume;
    audioSource.Play();
    float clipLength = audioSource.clip.length;

    Destroy(audioSource.gameObject, clipLength);
  }

  public void PlayRandomAudioClip(AudioClip[] audioClips, Transform spawnTransform, float volume) {

    int random = Random.Range(0, audioClips.Length);

    AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

    audioSource.clip = audioClips[random];
    audioSource.volume = volume;
    audioSource.Play();
    float clipLength = audioSource.clip.length;

    Destroy(audioSource.gameObject, clipLength);
  }
}
