using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Camera targetCamera;

    public Texture2D backgroundTexture;
    public Texture2D midgroundTexture;
    public Texture2D foregroundTexture;

    public GameObject backgroundPlane;
    public GameObject midgroundPlane;
    public GameObject foregroundPlane;
    public List<GameObject> planes = new();

    [Range(0.0f, 0.1f)]
    public float backgroundScrollRate = 0.01f;
    [Range(0.0f, 0.1f)]
    public float midgroundScrollRate = 0.02f;
    [Range(0.0f, 0.1f)]
    public float foregroundScrollRate = 0.03f;

    void Start()
    {
        backgroundPlane = CreatePlane(backgroundTexture);
        midgroundPlane = CreatePlane(midgroundTexture);
        foregroundPlane = CreatePlane(foregroundTexture);
    }

    void Update()
    {
        foreach (GameObject plane in planes)
        {
            plane.transform.position = new Vector3(targetCamera.transform.position.x, 0, targetCamera.transform.position.z);
        }

        backgroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(targetCamera.transform.position.x * backgroundScrollRate, targetCamera.transform.position.z * backgroundScrollRate));
        midgroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(targetCamera.transform.position.x * midgroundScrollRate, targetCamera.transform.position.z * midgroundScrollRate));
        foregroundPlane.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(targetCamera.transform.position.x * foregroundScrollRate, targetCamera.transform.position.z * foregroundScrollRate));
    }

    GameObject CreatePlane(Texture2D texture)
    {
        // Create plane.
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

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
