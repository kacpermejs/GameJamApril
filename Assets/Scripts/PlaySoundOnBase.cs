using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class PlaySoundOnBase : MonoBehaviour
{
  public float cooldownTime = 1f;
  public float startTime = 0f;

  private AudioSource audioSource;
  private bool canPlaySound = true;


  private void Awake() {
    audioSource = GetComponent<AudioSource>();
  }

  public void Play() {
    if (canPlaySound) {
      audioSource.time = startTime;
      audioSource.Play();

      canPlaySound = false;
      StartCoroutine(CooldownCoroutine());
    }
  }

  private IEnumerator CooldownCoroutine() {
    yield return new WaitForSeconds(cooldownTime);
    canPlaySound = true;
  }
}
