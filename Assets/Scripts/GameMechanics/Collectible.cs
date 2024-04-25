using System;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour {
  public Item item;

  public UnityEvent onCollected;
  public UnityEvent<bool> onTargeted;

  public bool destroyOnCollected = true;

  private bool isTarget = false;

  public bool IsTarget { get => isTarget; private set => isTarget = value; }

  public void MarkAsTarget() {
    IsTarget = true;
    HandleTargetSet();
  }

  public void UnsetTarget() {
    IsTarget = false;
    HandleTargetUnset();
  }

  private void HandleTargetSet() {
    onTargeted.Invoke(true);
  }
  
  private void HandleTargetUnset() {
    onTargeted.Invoke(false);
  }


  public void Collect() {

    onCollected.Invoke();

    if (destroyOnCollected) {
      Destroy(gameObject);
    }
  }
}
