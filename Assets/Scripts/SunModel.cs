using UnityEngine;

[CreateAssetMenu(fileName = "SunModel", menuName = "SunModel", order = 0)]
public class SunModel : ScriptableObject
{
  public const float MIN_RADIUS = 1.5f;
  public const float MAX_RADIUS = 3.5f;

  public float Radius;
  public int Color;

  public void Generate(ref System.Random random)
  {
    Radius = (float)random.NextDouble() * (MAX_RADIUS - MIN_RADIUS) + MIN_RADIUS;
  }
}