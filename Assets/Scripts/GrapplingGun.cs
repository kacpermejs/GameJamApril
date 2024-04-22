using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter))]
public class GrapplingGun : MonoBehaviour {

  [SerializeField]
  private InputActionReference shootActionReference;
  [SerializeField]
  private InputActionReference retractActionReference;
  [SerializeField]
  private InputActionReference tightenActionReference;
  [SerializeField]
  private InputActionReference loosenActionReference;

  [SerializeField]
  private Rope _rope;

  [SerializeField]
  private DistanceJoint2D mainJoint;

  private Shooter _shooter;
  private Harpoon _currentHarpoon;

  private void Awake() {
    _shooter = GetComponent<Shooter>();
  }

  private void OnEnable() {
    _rope.enabled = false;
    mainJoint.enabled = false;
    _shooter.Load();
  }

  public void OnShoot(InputAction.CallbackContext context) {
    if( context.performed && _shooter.IsLoaded) {
      _currentHarpoon = _shooter.Shoot().GetComponent<Harpoon>();
      _currentHarpoon.onStuck.AddListener(OnHarpoonAttached);

      _rope.enabled = true;
      _rope.end = _currentHarpoon.endAnchor;
    }
  }

  public void OnRetract(InputAction.CallbackContext context) {
    if (context.performed && _currentHarpoon != null && !_shooter.IsLoaded) {
      _rope.enabled = false;
      Destroy(_currentHarpoon.gameObject);
      _currentHarpoon = null;
      mainJoint.enabled = false;
      _shooter.Load();
    }
  }

  public void OnTighten(InputAction.CallbackContext context) {
    
  }

  public void OnLoosen(InputAction.CallbackContext context) {

  }

  private void OnHarpoonAttached() {
    mainJoint.anchor = _rope.transform.position - mainJoint.transform.position;
    mainJoint.connectedBody = _currentHarpoon.transform.parent.GetComponent<Rigidbody2D>();

    Vector3 arrowEndWorldPosition = _currentHarpoon.endAnchor.position;
    Vector3 arrowEndRockLocalPosition = _currentHarpoon.transform.parent.transform.InverseTransformPoint(arrowEndWorldPosition);

    mainJoint.connectedAnchor = arrowEndRockLocalPosition;
    mainJoint.enabled = true;
  }

}
