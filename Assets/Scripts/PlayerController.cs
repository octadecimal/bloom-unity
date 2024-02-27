using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  public InputActionAsset inputActions;

  private InputAction moveAction;
  private InputAction lookAction;

  private Quaternion targetRotation;

  private float acceleration;

  private void Start()
  {
    acceleration = 0.0f;

    moveAction = inputActions.FindAction("Move");
    lookAction = inputActions.FindAction("Interact");
  }

  private void Update()
  {
    // Read MOVE action.
    Vector2 moveInput = moveAction.ReadValue<Vector2>();
    float lookInput = lookAction.ReadValue<float>();

    // Adjust target rotation.
    if (moveInput.sqrMagnitude > 0.01f)
    {
      float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg * -1f;
      targetRotation = Quaternion.Euler(0f, angle + 90, 0f);
    }

    // Lerp rotation towards target rotation.
    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, 0.0075f);

    // Get distance from target rotation.
    float angleDistance = 1f - (Quaternion.Angle(gameObject.transform.rotation, targetRotation) / 180f);

    // Get angular thrust multiplier.
    float thrustMultiplier = Mathf.Pow(angleDistance, 3f);
    // float thrustMultiplier = 1.0f;

    // Add to acceleration.
    acceleration += moveInput.sqrMagnitude * thrustMultiplier + (50f * lookInput);

    // Move forward.
    float speed = acceleration * 0.0015f;
    gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;

    // Friction.
    acceleration *= 0.995f;

    HexUtils.HexCoordinates hex = HexUtils.WorldToHex(gameObject.transform.position);
    Debug.Log(hex.r + "," + hex.q);
  }
}
