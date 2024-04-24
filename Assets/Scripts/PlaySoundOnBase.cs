using System.Collections;
using UnityEngine;

public abstract class PlaySoundOnBase : MonoBehaviour
{
  public float cooldownTime = 1f;
  public float volume = 1f;

  private bool canPlaySound = true;

  [SerializeField] private AudioClip[] clips;

  public void Play() {
    if (canPlaySound) {
      SoundFXManager.instance.PlayRandomAudioClip(clips, transform, volume);

      canPlaySound = false;
      StartCoroutine(CooldownCoroutine());
    }
  }

  private IEnumerator CooldownCoroutine() {
    yield return new WaitForSeconds(cooldownTime);
    canPlaySound = true;
  }
}
