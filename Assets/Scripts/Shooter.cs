using UnityEngine;
using UnityEngine.InputSystem;

class Shooter : MonoBehaviour {

  public Projectile projectile;

  private Projectile projectileInstance;

  public bool loadImmediately = false;

  [SerializeField]
  private Transform _projectileHandler;

  private void Start() {
    
    if (loadImmediately) {
      Load();
    }
  }

  public void Load() {
    projectileInstance = Instantiate(projectile, transform);
  }

  public Projectile Shoot() {
    if (projectileInstance == null) {
      return null;
    }
    // unparent
    projectileInstance.transform.parent = _projectileHandler;
    projectileInstance.Launch();
    // forget
    var passedProjectile = projectileInstance;
    projectileInstance = null;
    if (loadImmediately) {
      Load();
    }
    return passedProjectile;
  }

  public bool IsLoaded { get { return projectileInstance != null; } }
}

