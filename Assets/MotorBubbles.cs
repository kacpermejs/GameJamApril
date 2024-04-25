using UnityEngine;

public class MotorBubbles : MonoBehaviour {
  private ParticleSystem _particleSystem;
  private SubmarineController _submarineController;

  private void Awake() {
    _particleSystem = GetComponent<ParticleSystem>();
    _submarineController = GetComponentInParent<SubmarineController>();
  }
  void Update() {
    if(_submarineController.Throttle > 0.01 || _submarineController.Throttle < -0.01) {
      _particleSystem.Play();
    } else {
      _particleSystem.Stop();
    }
  }
}
