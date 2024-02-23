using UnityEngine;

public class Mover : MonoBehaviour
{
  private float x = 0.0f;
  private float y = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      x -= 0.00003f;
      y += 0.00001f;

        // Access the Renderer component attached to the GameObject
        Renderer renderer = GetComponent<Renderer>();

        // Check if the Renderer exists
        if (renderer != null)
        {
            // Access the Material of the Renderer
            Material mat = renderer.material;

            // Change the color of the material
            mat.SetVector("_Offset", new Vector2(x, y));
        }
    }
}
