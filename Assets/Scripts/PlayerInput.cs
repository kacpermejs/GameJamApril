using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(SubmarineController))]
public class PlayerInput : MonoBehaviour {

  [SerializeField]
  private float _tiltInputFactor = 1f;
  [SerializeField]
  private float _elevationInputFactor = 1f;
  [SerializeField]
  private float _throttleInputFactor = 1f;
  [SerializeField]
  private float[] _steps = { -1f, -0.33f, 0f, 0.33f, 1f };
  [SerializeField]
  private int _initialStep = 2;

  private int _currentStep;
  private float _inputStep = 0f;


  [SerializeField]
  private InputActionReference throttle;
  [SerializeField]
  private InputActionReference tilt;
  [SerializeField]
  private InputActionReference elevation;

  private SubmarineController controller;

  private void Awake() {
    controller = GetComponent<SubmarineController>();
    _currentStep = _initialStep;
  }

  private void Update() {
    //throttle
    UpdateThrottle();
    UpdateBuoyancy();
    UpdateBuoyancyDisproportion();
  }

  private void UpdateBuoyancy() {
    float value = elevation.action.ReadValue<float>();
    if (value > 0.01 || value < -0.01) {
      //steering up means the water weight has to go down for submarine to go up
      controller.UpdateBuoyancy(_elevationInputFactor * -value * Time.deltaTime);
    }
  }
  
  private void UpdateBuoyancyDisproportion() {
    float value = tilt.action.ReadValue<float>();
    if (value > 0.01 || value < -0.01) {
      controller.Tilt(_tiltInputFactor * value * Time.deltaTime);
    }
  }

  private void UpdateThrottle() {
    float value = throttle.action.ReadValue<float>();
    if (value < -0.01 || value > 0.01) {
      _inputStep += _throttleInputFactor * Time.deltaTime;
      if (_inputStep > 1f) {
        if (value > 0f) {
          StepUpThrottle();
          _inputStep = 0;
        } else {
          StepDownThrottle();
          _inputStep = 0;
        }
      }
    } else {
      _inputStep = 0;
    }
  }

  void StepUpThrottle() {
    if (_currentStep < _steps.Length - 1) {
      _currentStep++;
      controller.UpdateThrottle(_steps[_currentStep]);
    }
  }

  void StepDownThrottle() {
    if (_currentStep > 0) {
      _currentStep--;
      controller.UpdateThrottle(_steps[_currentStep]);
    }
  }


}

