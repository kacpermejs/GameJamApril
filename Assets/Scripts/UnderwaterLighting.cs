using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnderwaterLighting : MonoBehaviour
{
  public AnimationCurve intensityCurve; // Editable curve for intensity variation
  public float intensityMultiplier = 1f; // Multiplier to adjust the overall intensity
  public float surfaceLevel = 0f; // Y position at the surface where intensity is maximum
  public float bottomLevel = -10f; // Y position at the bottom where intensity is minimum

  public Transform playerTransform; // Reference to the player's transform

  [SerializeField]
  private Light2D globalLight; // Reference to the directional light

  private void Awake() {
    if (globalLight == null) {
      throw new System.ArgumentNullException("Setup global light in the inspector");
    }
  }

  void Update() {
    if (playerTransform != null && globalLight != null) {
      // Get player's Y position
      float playerY = playerTransform.position.y;

      // If player is above surface level, set intensity to 1
      if (playerY > surfaceLevel) {
        globalLight.intensity = 1f;
      }
      // If player is below bottom level, set intensity to 0
      else if (playerY < bottomLevel) {
        globalLight.intensity = 0f;
      } else {
        // Normalize player's Y position between surface and bottom levels
        float normalizedY = Mathf.InverseLerp(surfaceLevel, bottomLevel, playerY);

        // Evaluate the curve at normalized Y position
        float intensity = intensityCurve.Evaluate(normalizedY);

        // Adjust intensity based on multiplier
        intensity *= intensityMultiplier;

        // Set the global light intensity
        globalLight.intensity = intensity;
      }
    }
  }
}
