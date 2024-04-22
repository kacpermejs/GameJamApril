using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour {

  public Transform end;

  private LineRenderer _lineRenderer;

  private void Awake() {
    _lineRenderer = GetComponent<LineRenderer>();
    
  }

  void Update() {
    if( end == null) {
      return;
    }
    _lineRenderer.SetPosition(0, this.transform.position);
    _lineRenderer.SetPosition(1, end.position);
  }

  private void OnEnable() {
    _lineRenderer.enabled = true;
  }

  private void OnDisable() {
    _lineRenderer.enabled = false;
  }
}
