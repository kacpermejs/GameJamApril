using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SubmarineUI : MonoBehaviour {

  [SerializeField]
  private SubmarineController submarine;

  private VisualElement root;

  private ProgressBar rearTankBar;
  private ProgressBar frontTankBar;
  private ProgressBar throttleBar;

  private void Start() {
    root = GetComponent<UIDocument>().rootVisualElement;

    rearTankBar = root.Q<ProgressBar>("RearTankBar");
    frontTankBar = root.Q<ProgressBar>("FrontTankBar");
    throttleBar = root.Q<ProgressBar>("ThrottleBar");
  }
  private void Update() {
    rearTankBar.value = submarine.RearTankFill;
    frontTankBar.value = submarine.FrontTankFill;
    throttleBar.value = submarine.Throttle + 1f;
  }
}
