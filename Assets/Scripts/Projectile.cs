using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
  private Rigidbody2D _rb;

  [SerializeField]
  private float _forceMultiplier = 100;
  private bool doArrowPhysics = false;

  private bool _isShot;

  public bool IsShot { get => _isShot; }

  public UnityEvent onShot;
  public UnityEvent onImpact;

  public void Launch() {
    doArrowPhysics = true;
    _rb.isKinematic = false;
    _rb.AddForce(transform.right * _forceMultiplier);

    onShot.Invoke();
  }

  private void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _rb.isKinematic = true;
    doArrowPhysics = false;

    onShot.AddListener(HandleShot);
    onImpact.AddListener(HandleImpact);
  }

  private void Update() {
    if (doArrowPhysics) {
      float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    onImpact.Invoke();
    onImpact.RemoveAllListeners();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    onImpact.Invoke();
    onImpact.RemoveAllListeners();
  }

  private void HandleShot() {
    _isShot = true;
  }

  private void HandleImpact() {
    if (IsShot) {
      doArrowPhysics = false;
    }
  }
}
