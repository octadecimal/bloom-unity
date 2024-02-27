using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class HexLayout : MonoBehaviour
{
  [Header("Grid Settings")]
  public Vector2Int gridSize;

  [Header("Tile Settings")]
  public Material material;


  private void OnEnable()
  {
    LayoutGrid();
  }

  private void OnValidate()
  {
    if (Application.isPlaying)
    {
      LayoutGrid();
    }
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

  private Vector3 HexToWorldInternal(Vector2Int coordinate)
  {
    int col = coordinate.x;
    int row = coordinate.y;
    float width;
    float height;
    float xPos;
    float yPos;
    bool shouldOffset;
    float horizontalDistance;
    float verticalDistance;
    float offset;
    float size = 1.0f;

    shouldOffset = (col % 2) == 0;
    width = 2f * size;
    height = Mathf.Sqrt(3f) * size;

    horizontalDistance = width * (3f / 4f);
    verticalDistance = height;
    offset = shouldOffset ? height / 2f : 0;

    xPos = col * horizontalDistance;
    yPos = (row * verticalDistance) - offset;

    return new Vector3(xPos, 0, -yPos);
  }
}