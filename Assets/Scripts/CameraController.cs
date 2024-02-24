using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameObject player;

  private void Start()
  {

  }

  private void Update()
  {
    Vector3 lerp = Vector3.Lerp(gameObject.transform.position, player.transform.position, 1.0f);
    gameObject.transform.position = new Vector3(lerp.x, 4f, lerp.z);
  }
}