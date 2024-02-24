using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  public InputActionAsset inputActions;

  private InputAction moveAction;

  private Quaternion targetRotation;

  private void Start()
  {
    moveAction = inputActions.FindAction("Move");
  }

  private void FixedUpdate()
  {
  }

  private void Update()
  {
    // Read MOVE action.
    Vector2 moveInput = moveAction.ReadValue<Vector2>();

    // Adjust target rotation.
    if (moveInput.sqrMagnitude > 0.01f)
    {
      float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg * -1f;
      targetRotation = Quaternion.Euler(0f, angle + 90, 0f);
    }

    // Lerp rotation towards target rotation.
    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, 0.0025f);

    // Get distance from target rotation.
    float angleDistance = 1f - (Quaternion.Angle(gameObject.transform.rotation, targetRotation) / 180f);

    // Get angular thrust multiplier.
    float thrustMultiplier = Mathf.Pow(angleDistance, 2);

    // Move forward.
    float acceleration = 1f * thrustMultiplier * moveInput.sqrMagnitude;
    gameObject.transform.position += gameObject.transform.forward * acceleration * Time.deltaTime;
  }
}
