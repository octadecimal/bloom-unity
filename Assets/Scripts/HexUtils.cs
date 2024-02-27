using UnityEngine;

public static class HexUtils
{
  public const float hexSize = 1.0f;

  public static Vector3 HexToWorld(Vector2Int hexPosition)
  {
    float x = hexSize * 3 / 2 * hexPosition.x;
    float z = hexSize * Mathf.Sqrt(3f) * (hexPosition.y + 0.5f * (hexPosition.x & 1));
    return new Vector3(x, 0, z);
  }

  public static Vector2Int WorldToHex(Vector3 worldPosition)
  {
    float x = worldPosition.x;
    float z = worldPosition.z;

    // Convert from world space to hex space
    float q = (2f / 3f * x) / hexSize;
    float r = (-1f / 3f * x + Mathf.Sqrt(3f) / 3f * z) / hexSize;

    // Round to the nearest hex coordinate
    return HexRound(q, r);
    // return Round2(new Vector2(q, r));
  }

  public static Vector2Int Round2(Vector2 frac)
  {
    float q = Mathf.Round(frac.x);
    float r = Mathf.Round(frac.y);
    float s = Mathf.Round(-q - r);

    float q_diff = Mathf.Abs(q - frac.x);
    float r_diff = Mathf.Abs(r - frac.y);
    float s_diff = Mathf.Abs(s - (-q - r));

    if (q_diff > r_diff && q_diff > s_diff)
      q = -r - s;
    else if (r_diff > s_diff)
      r = -q - s;
    else
      s = -q - r;

    return new Vector2Int(Mathf.RoundToInt(q), Mathf.RoundToInt(r));
  }

  private static Vector2Int HexRound(float q, float r)
  {
    float x = q;
    float z = r;
    float y = -x - z;

    int rx = Mathf.RoundToInt(x);
    int ry = Mathf.RoundToInt(y);
    int rz = Mathf.RoundToInt(z);

    float xDiff = Mathf.Abs(rx - x);
    float yDiff = Mathf.Abs(ry - y);
    float zDiff = Mathf.Abs(rz - z);

    if (xDiff > yDiff && xDiff > zDiff)
      rx = -ry - rz;
    else if (yDiff > zDiff)
      ry = -rx - rz;
    else
      rz = -rx - ry;

    int col = rx;
    int row = ry + (rx - (rx & 1)) / 2;

    return new Vector2Int(col, row);
  }
}
