using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
  public Camera targetCamera;

  public Texture2D backgroundTexture;
  public Texture2D midgroundTexture;
  public Texture2D foregroundTexture;

  private GameObject backgroundPlane;
  private GameObject midgroundPlane;
  private GameObject foregroundPlane;
  private List<GameObject> planes = new();

  [Range(0.0f, 0.1f)]
  public float backgroundScrollRate = -30f;
  [Range(0.0f, 0.1f)]
  public float midgroundScrollRate = -20f;
  [Range(0.0f, 0.1f)]
  public float foregroundScrollRate = -1f;

  void Start()
  {
    backgroundPlane = CreatePlane(backgroundTexture);
    midgroundPlane = CreatePlane(midgroundTexture);
    foregroundPlane = CreatePlane(foregroundTexture);

    backgroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
    midgroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
    foregroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
  }

  void Update()
  {
    backgroundPlane.transform.position = new Vector3(targetCamera.transform.position.x, 0, targetCamera.transform.position.z);
    midgroundPlane.transform.position = new Vector3(targetCamera.transform.position.x, 0.01f, targetCamera.transform.position.z);
    foregroundPlane.transform.position = new Vector3(targetCamera.transform.position.x, 0.02f, targetCamera.transform.position.z);

    backgroundPlane.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(30, 30));
    midgroundPlane.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(20, 20));
    foregroundPlane.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(10, 10));

    backgroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(targetCamera.transform.position.x * -backgroundScrollRate, targetCamera.transform.position.z * -backgroundScrollRate));
    midgroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(targetCamera.transform.position.x * -midgroundScrollRate, targetCamera.transform.position.z * -midgroundScrollRate));
    foregroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(targetCamera.transform.position.x * -foregroundScrollRate, targetCamera.transform.position.z * -foregroundScrollRate));
  }

  GameObject CreatePlane(Texture2D texture)
  {
    // Create plane.
    GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
    plane.transform.localScale = new Vector3(10, 1, 10);

    // Create material.
    Material material = new(Shader.Find("Unlit/Transparent"))
    {
      mainTexture = texture
    };

    // Add material to plane.
    plane.GetComponent<Renderer>().material = material;

    // Save and return plane.
    planes.Add(plane);
    return plane;
  }
}
