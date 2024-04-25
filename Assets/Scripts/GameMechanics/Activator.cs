using UnityEngine;
using UnityEngine.Events;

public abstract class Activator : MonoBehaviour {
  public UnityEvent onActivated;

  public abstract bool Condition();

  private void Update() {
    if (Condition()) {
      onActivated.Invoke();
    }
  }
}

