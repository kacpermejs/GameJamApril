using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Collector : MonoBehaviour {

  public UnityEvent<Collectible> onConsume;
  public LayerMask includeLayers;

  private Collectible target;

  public void HandleCollectAction(InputAction.CallbackContext context) {
    if (context.performed && target != null) {
      Consume(target);
    }
  }

  public void Consume(Collectible collectible) {
    Collection.instance.TryAddToCollection(collectible.item, out bool success);
    if (success) {
      collectible.Collect();
      onConsume.Invoke(collectible);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (IsLayerInMask(collision.gameObject.layer, includeLayers)) {
      var collectible = collision.GetComponent<Collectible>();
      if (collectible.enabled == true) {
        SetAsTarget(collectible);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (IsLayerInMask(collision.gameObject.layer, includeLayers)) {
      var collectible = collision.GetComponent<Collectible>();

      UnsetAsTarget(collectible);
    }
  }

  private void SetAsTarget(Collectible collectible) {
    if (target != null) {
      UnsetAsTarget(target);
    }

    collectible.MarkAsTarget();
    target = collectible;
  }
  
  private void UnsetAsTarget(Collectible collectible) {
    collectible.UnsetTarget();
    target = null;
  }

  bool IsLayerInMask(int layer, LayerMask layerMask) {
    return layerMask == (layerMask | (1 << layer));
  }

}
