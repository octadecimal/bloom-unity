using UnityEngine;

public class HexUtils : MonoBehaviour
{
  public static float hexSize = 2.0f; // Size of the hex from center to corner

  public struct HexCoordinates
  {
    public int q; // Column
    public int r; // Row

    public HexCoordinates(int q, int r)
    {
      this.q = q;
      this.r = r;
    }
  }

  // Convert a world position to hex coordinates
  public static HexCoordinates WorldToHex(Vector3 worldPosition)
  {
    float q = (Mathf.Sqrt(3f) / 3f * worldPosition.x - 1f / 3f * worldPosition.z) / hexSize;
    float r = (2f / 3f * worldPosition.z) / hexSize;

    return CubeToAxial(RoundCubeCoordinate(q, -q - r, r));
  }

  // Convert hex coordinates to a world position
  public static Vector3 HexToWorld(HexCoordinates hexCoords)
  {
    float x = hexSize * (Mathf.Sqrt(3f) * hexCoords.q + Mathf.Sqrt(3f) / 2f * hexCoords.r);
    float z = hexSize * (3f / 2f * hexCoords.r);
    return new Vector3(x, 0, z);
  }

  private static HexCoordinates CubeToAxial(Vector3Int cubeCoord)
  {
    int q = cubeCoord.x;
    int r = cubeCoord.z;
    return new HexCoordinates(q, r);
  }

  private static Vector3Int RoundCubeCoordinate(float q, float r, float s)
  {
    int rx = Mathf.RoundToInt(q);
    int ry = Mathf.RoundToInt(r);
    int rz = Mathf.RoundToInt(s);

    float x_diff = Mathf.Abs(rx - q);
    float y_diff = Mathf.Abs(ry - r);
    float z_diff = Mathf.Abs(rz - s);

    if (x_diff > y_diff && x_diff > z_diff)
      rx = -ry - rz;
    else if (y_diff > z_diff)
      ry = -rx - rz;
    else
      rz = -rx - ry;

    return new Vector3Int(rx, ry, rz);
  }
}
