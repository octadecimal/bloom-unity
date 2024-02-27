using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class HexLayout : MonoBehaviour
{
  [Header("Grid Settings")]
  public Vector2Int gridSize;

  [Header("Tile Settings")]
  public Material material;

  public Camera targetCamera;


  private void Awake()
  {
    LayoutGrid();
  }

  private void Update()
  {
    int abs_x = (int)targetCamera.transform.position.x;
    int abs_z = (int)targetCamera.transform.position.z;

    float x = abs_x * 3f;
    float z = abs_z * Mathf.Sqrt(3);

    var cameraHex = Hex.FromWorld(targetCamera.transform.position);
    var cameraPos = Hex.ToWorld(cameraHex.q, cameraHex.r);

    gameObject.transform.position = new Vector3(cameraPos.x, 0, -cameraPos.z);
  }

  private void LayoutGrid()
  {

    for (int y = -gridSize.y; y <= gridSize.y; y++)
    {
      for (int x = -gridSize.x; x <= gridSize.x; x++)
      {
        GameObject tile = new($"Hex {x},{y}", typeof(HexRenderer));
        tile.transform.position = Hex.ToWorld(x, y);
        // tile.transform.position = HexUtils.HexToWorld(new Vector2Int(x, y));
        // tile.transform.position = HexUtils2.HexToWorld(x, y);
        tile.transform.SetParent(this.transform, true);

        HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
        MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();
        meshRenderer.material = material;

        hexRenderer.DrawMesh();

      }
    }
  }
}