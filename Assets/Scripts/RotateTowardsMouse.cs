using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour {

  private Camera mainCamera;

  private void Awake() {
    mainCamera = Camera.main;
  }

  void Update() {
    // Get the direction from the object's position to the mouse position
    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    Vector3 direction = mousePosition - transform.position;
    direction.z = 0f; // Ensure the object stays in the 2D plane

    // Calculate the angle in degrees
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Rotate the object towards the mouse
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }
}
