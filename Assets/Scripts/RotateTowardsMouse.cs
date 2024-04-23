using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour {

  private Camera mainCamera;
  
  private Vector3 screenPosition;
  private Vector3 worldPosition;
  private Vector3 direction;

  private Plane plane;

  private void Awake() {
    mainCamera = Camera.main;

    plane = new Plane(Vector3.forward, transform.position.z);
  }

  private void LateUpdate() {
    // Get the direction from the object's position to the mouse position

    screenPosition = Input.mousePosition;
    Ray ray = mainCamera.ScreenPointToRay(screenPosition);
    if(plane.Raycast(ray, out float distance)) {
      worldPosition = ray.GetPoint(distance);
    }

    direction = worldPosition - transform.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }

  private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(worldPosition, 0.5f);
  }
}
