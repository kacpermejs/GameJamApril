using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDepthEffect : MonoBehaviour
{
  public AudioSource soundtrackAudioSource;
  public AudioSource underwaterAmbienceAudioSource;
  public AudioLowPassFilter lowPassFilter; // Reference to the low-pass filter component
  public Transform playerTransform;

  public AnimationCurve soundtrackVolumeCurve;
  public AnimationCurve ambienceVolumeCurve;

  public float maxVolume = 1f; // The maximum volume of the soundtrack
  public float minVolume = 0.1f; // The minimum volume of the soundtrack
  public float underwaterAmbienceVolume = 0.5f; // Volume of underwater ambience
  public float baseMusicVolume = 1f; // Base volume of the soundtrack
  public float baseAmbientVolume = 1f; // Base volume of underwater ambience

  public float maxPlayerPositionY = 10f; // Maximum player Y position
  public float minPlayerPositionY = -50f; // Minimum player Y position

  public float minFrequency = 250f;
  public float maxFrequency = 5000f;

  private void Update() {
    float playerY = playerTransform.position.y;

    if (playerY < maxPlayerPositionY)
    {
      float maxDepth = Mathf.Abs(minPlayerPositionY - maxPlayerPositionY);
      float depthRatio = Mathf.Clamp01((maxPlayerPositionY - playerY) / maxDepth);
      float soundtrackVolumeModifier = soundtrackVolumeCurve.Evaluate(depthRatio);
      float ambienceVolumeModifier = ambienceVolumeCurve.Evaluate(depthRatio);
      float muffledVolume = Mathf.Lerp(minVolume, maxVolume, soundtrackVolumeModifier) * baseMusicVolume; // Adjust volume based on depth
      soundtrackAudioSource.volume = muffledVolume;
      underwaterAmbienceAudioSource.volume = underwaterAmbienceVolume * ambienceVolumeModifier * baseAmbientVolume;

      // Adjust low-pass filter cutoff frequency based on depth
      lowPassFilter.cutoffFrequency = Mathf.Lerp(maxFrequency, minFrequency, depthRatio);
    } else {
      // Player is not underwater
      soundtrackAudioSource.volume = maxVolume * baseMusicVolume;
      underwaterAmbienceAudioSource.volume = 0f; // No underwater ambience

      // Reset low-pass filter cutoff frequency
      lowPassFilter.cutoffFrequency = maxFrequency;
    }
  }
}
