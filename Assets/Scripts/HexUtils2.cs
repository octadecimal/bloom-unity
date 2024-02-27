using UnityEngine;

public class HexUtils2
{
  public static float hexSize = 1.0f; // Size of the hexagon from center to corner

  private static float hexWidth => Mathf.Sqrt(3) * hexSize;
  private static float hexHeight => 2 * hexSize;

  // Convert odd-q offset hex coordinates to world coordinates
  public static Vector3 HexToWorld(int q, int r)
  {
    float x = hexWidth * (q + 0.5f * (r & 1));
    float y = hexHeight * 0.75f * r;
    return new Vector3(x, 0, y);
  }

  // Convert world coordinates to odd-q offset hex coordinates
  public static Vector2Int WorldToHex(float x, float y)
  {
    float q = (x * Mathf.Sqrt(3) / 3 - y / 3) / hexSize;
    float r = y * 2 / 3 / hexSize;
    return RoundHex(q, r);
  }

  // Round fractional hex coordinates to the nearest hex grid coordinates
  private static Vector2Int RoundHex(float q, float r)
  {
    int rq = Mathf.RoundToInt(q);
    int rr = Mathf.RoundToInt(r);
    int rs = Mathf.RoundToInt(-q - r);

    float q_diff = Mathf.Abs(rq - q);
    float r_diff = Mathf.Abs(rr - r);
    float s_diff = Mathf.Abs(rs + q + r);

    if (q_diff > r_diff && q_diff > s_diff)
    {
      rq = -rr - rs;
    }
    else if (r_diff > s_diff)
    {
      rr = -rq - rs;
    }
    // No need to adjust rs because it's the dependent coordinate in odd-q

    return new Vector2Int(rq, rr);
  }
}
