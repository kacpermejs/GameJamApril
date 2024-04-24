using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Projectile))]
public class Harpoon : MonoBehaviour {

  [SerializeField]
  private LayerMask _stickableLayers;

  [SerializeField]
  private Collider2D _physicsCollider;

  public UnityEvent onStuck;

  public Transform endAnchor;

  private bool isStuck = false;

  private Projectile _projectile;
  private Rigidbody2D _rb;

  private void Awake() {
    _projectile = GetComponent<Projectile>();
    _rb = GetComponent<Rigidbody2D>();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (_projectile.IsShot && collision != null && IsLayerInMask(collision.gameObject.layer, _stickableLayers)) {
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
    //Disable Collider to avoid problems with physics
    _physicsCollider.enabled = false;

    // Set the harpoon's position and rotation to match the collided object
    transform.parent = obj.transform;
    gameObject.layer = obj.layer;

    onStuck.Invoke();
  }

}
