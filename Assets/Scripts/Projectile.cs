using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
  private Rigidbody2D _rb;

  [SerializeField]
  private float _forceMultiplier = 100;
  private bool doArrowPhysics = false;

  public void Launch() {
    doArrowPhysics = true;
    _rb.isKinematic = false;
    _rb.AddForce(transform.right * _forceMultiplier);
  }

  private void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _rb.isKinematic = true;
    doArrowPhysics = false;
  }

  private void Update() {
    if (doArrowPhysics) {
      float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
  }
  private void OnCollisionEnter2D(Collision2D collision) {
    doArrowPhysics = false;
  }
}
