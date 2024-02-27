using UnityEngine;

public static class HexVectorExtensions
{
  public static Hex ToHex(this Vector3 world)
  {
    return Hex.FromWorld(world);
  }
}

[System.Serializable]
public struct Hex
{
  public const float SIZE = 1.0f;

  public int q;
  public int r;

  public Hex(int q, int r)
  {
    this.q = q;
    this.r = r;
  }

  public Hex(float q, float r) : this(Mathf.RoundToInt(q), Mathf.RoundToInt(r))
  {
  }

  public static Hex FromWorld(Vector3 world)
  {
    float x = world.x;
    float z = world.z;
    float q = 2f / 3f * x / SIZE;
    float r = (-1f / 3f * x + Mathf.Sqrt(3f) / 3f * z) / SIZE;

    return Round(q, r);
  }

  public static Vector3 ToWorld(int q, int r, float y = 0)
  {
    float x = SIZE * 3 / 2 * q;
    float z = SIZE * Mathf.Sqrt(3f) * (r + 0.5f * (q & 1));

    return new Vector3(x, y, z);
  }

  public readonly Vector3 ToWorld(Hex hex, float y = 0)
  {
    return ToWorld(q, r, y);
  }

  private static Hex Round(float q, float r)
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

    return new Hex(rx, ry + (rx - (rx & 1)) / 2);
  }
}