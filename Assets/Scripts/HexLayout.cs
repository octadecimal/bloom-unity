using UnityEngine;

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
    var cameraHex = Hex.FromWorld(targetCamera.transform.position);
    var cameraPos = Hex.ToWorld(cameraHex.q, cameraHex.r);

    gameObject.transform.position = new Vector3(cameraPos.x, -0.5f, -cameraPos.z);
  }

  private void LayoutGrid()
  {

    for (int z = -gridSize.y; z <= gridSize.y; z++)
    {
      for (int x = -gridSize.x; x <= gridSize.x; x++)
      {
        GameObject tile = new($"Hex {x},{z}", typeof(HexRenderer));
        tile.transform.position = Hex.ToWorld(x, z);
        tile.transform.SetParent(this.transform, true);

        HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
        MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();
        meshRenderer.material = material;

        hexRenderer.DrawMesh();

      }
    }
  }
}