using UnityEngine;

public class SubmarineController : MonoBehaviour {

  [SerializeField] private float _defaultBuoyancy;
  [SerializeField] private float _buoyancyForceMagnitude;
  [SerializeField] private float _throttleForceMagnitude;

  [Range(-1f, 1f)]
  [SerializeField]
  private float _throttle;

  [Range(0f, 1f)]
  [SerializeField]
  private float _frontTankFill;

  [Range(0f, 1f)]
  [SerializeField]
  private float _rearTankFill;

  [SerializeField]
  private Transform _frontTankTransform;

  [SerializeField]
  private Transform _rearTankTransform;

  private Rigidbody2D _rb;

  public float Throttle { get => _throttle; }
  public float FrontTankFill { get => _frontTankFill; }
  public float RearTankFill { get => _rearTankFill; }

  private void Awake() {
    _rb = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {

    //buoyancy
    //_rb.AddForce(Vector2.up * _defaultBuoyancy);

    //front
    _rb.AddForceAtPosition(Vector2.up * _buoyancyForceMagnitude * -_frontTankFill, _frontTankTransform.position);

    //back
    _rb.AddForceAtPosition(Vector2.up * _buoyancyForceMagnitude * -_rearTankFill, _rearTankTransform.position);

    //throttle
    _rb.AddForce(transform.right * _throttle * _throttleForceMagnitude);

  }

  public void UpdateThrottle(float value) {
    _throttle = Mathf.Clamp(value, -1, 1);
  }

  public void UpdateBuoyancy(float offset) {
    _frontTankFill = Mathf.Clamp01(_frontTankFill + offset);
    _rearTankFill = Mathf.Clamp01(_rearTankFill + offset);
  }

  public void Tilt(float offset) {
    _frontTankFill = Mathf.Clamp01(_frontTankFill + offset);
    _rearTankFill = Mathf.Clamp01(_rearTankFill - offset);
  }

  public void Level() {
    //float combinedFill = (_frontTankFill + _rearTankFill) / 2f;
    _frontTankFill = 0.5f;
    _rearTankFill = 0.5f;
  }
}
