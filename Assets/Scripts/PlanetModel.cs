using UnityEngine;

[CreateAssetMenu(fileName = "PlanetModel", menuName = "PlanetModel", order = 0)]
public class PlanetModel : EntityModel
{
  public const float MIN_RADIUS = 0.25f;
  public const float MAX_RADIUS = 1.0f;

  public float Radius;

  public void Generate(ref System.Random random, float distance)
  {
    D = distance;
    Radius = (float)random.NextDouble() * (MAX_RADIUS - MIN_RADIUS) + MIN_RADIUS;
    W = (float)random.NextDouble() * Mathf.PI * 2;

    Hex hex = Hex.FromWorld(new Vector3(D * Mathf.Cos(Radius * W), 0, D * Mathf.Sin(Radius * W)));
    Q = hex.q;
    R = hex.r;

    Vector3 world = Hex.ToWorld(hex.q, hex.r);
    X = world.x;
    Y = world.y;
    Z = world.z;

    Debug.Log($"{Q},{R}");
  }
}