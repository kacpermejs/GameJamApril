using UnityEngine;
using UnityEngine.Events;

public class Harpoon : MonoBehaviour {

  private bool isStuck = false;
  private Rigidbody2D _rb;
  [SerializeField]
  private LayerMask _stickableLayers;

  public UnityEvent onStuck;

  [SerializeField]
  private Transform endAnchor;

  private void Awake() {
    _rb = GetComponent<Rigidbody2D>();
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision != null && IsLayerInMask(collision.gameObject.layer, _stickableLayers)) {
      // Check if the harpoon collides with an object
      if (!isStuck) {
        // Stick the harpoon into the object
        StickTo(collision.gameObject);
        isStuck = true;
      }
    }
  }

  bool IsLayerInMask(int layer, LayerMask layerMask) {
    return layerMask == (layerMask | (1 << layer));
  }


  void StickTo(GameObject obj) {
    // Disable Rigidbody component
    _rb.velocity = Vector3.zero;
    _rb.angularVelocity = 0;
    _rb.isKinematic = true;

    // Set the harpoon's position and rotation to match the collided object
    transform.parent = obj.transform;
    gameObject.layer = obj.layer;

    onStuck.Invoke();
  }

}
