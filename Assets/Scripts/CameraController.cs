using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
  public InputActionAsset inputActions;
  public GameObject player;

  private InputAction crouchAction;
  private Vector3 currentY = new Vector3(0f, 4f, 0f);

  private void Start()
  {
    crouchAction = inputActions.FindAction("Crouch");
  }

  private void Update()
  {
    float crouchInput = crouchAction.ReadValue<float>();

    Vector3 targetY = new Vector3(0f, crouchInput > 0 ? 36f : 12f, 0f);

    // Lerp
    currentY = Vector3.Lerp(currentY, targetY, 0.01f);

    Vector3 lerp = Vector3.Lerp(gameObject.transform.position, player.transform.position, Time.deltaTime * 15f);
    gameObject.transform.position = new Vector3(lerp.x, currentY.y, lerp.z);

    // gameObject.transform.position = new Vector3(player.transform.position.x, 4f, player.transform.position.z); ;
  }
}