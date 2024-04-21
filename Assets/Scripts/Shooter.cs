using UnityEngine;
using UnityEngine.InputSystem;

class Shooter : MonoBehaviour {

  public Projectile projectile;

  private Projectile projectileInstance;

  public bool loadImmediately = false;

  [SerializeField]
  private InputActionReference shootActionReference;
  [SerializeField]
  private Transform _projectileHandler;

  private void Start() {
    shootActionReference.action.performed += Shoot;
    if (loadImmediately) {
      Load();
    }
  }

  public void Load() {
    projectileInstance = Instantiate(projectile, transform);
  }

  public void Shoot(InputAction.CallbackContext context) {
    if (projectileInstance == null) {
      return;
    }
    // unparent
    projectileInstance.transform.parent = _projectileHandler;
    projectileInstance.Launch();
    // forget
    projectileInstance = null;
    if (loadImmediately) {
      Load();
    }
  }
}

