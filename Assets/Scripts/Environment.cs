using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
  public int Seed;
  private EnvironmentModel _model;

  private GameObject _sun;
  private List<GameObject> _planets;

  private void Awake()
  {
    _model = ScriptableObject.CreateInstance<EnvironmentModel>();
    _planets = new();
  }

  private void Start()
  {
    Generate();
  }

  private void Generate()
  {
    // Generate model.
    _model.Generate(Seed);

    // Create sun.
    _sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    _sun.transform.localScale = new Vector3(_model.Sun.Radius, _model.Sun.Radius, _model.Sun.Radius);
    _sun.transform.parent = gameObject.transform;

    // Create planets.
    Debug.Log(_model.Planets.Length);
    foreach (PlanetModel planetModel in _model.Planets)
    {
      GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      planet.transform.localScale = new Vector3(planetModel.Radius, planetModel.Radius, planetModel.Radius);
      planet.transform.position = new Vector3(planetModel.X, planetModel.Y, planetModel.Z);
      planet.transform.parent = gameObject.transform;
      _planets.Add(planet);
    }
  }
}
