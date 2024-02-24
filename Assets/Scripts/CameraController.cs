using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameObject player;

  private void Start()
  {

  }

  private void Update()
  {
    Vector3 lerp = Vector3.Lerp(gameObject.transform.position, player.transform.position, Time.deltaTime * 15f);
    gameObject.transform.position = new Vector3(lerp.x, 4f, lerp.z);

    // gameObject.transform.position = new Vector3(player.transform.position.x, 4f, player.transform.position.z); ;
  }
}