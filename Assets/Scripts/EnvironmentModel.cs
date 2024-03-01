using System.Linq;

public class EnvironmentModel : EntityModel
{
  public const int MIN_PLANETS = 6;
  public const int MAX_PLANETS = 12;

  public const float MIN_PLANET_SPACING = 1f;
  public const float MAX_PLANET_SPACING = 3f;

  public SunModel Sun;
  public PlanetModel[] Planets;

  private System.Random _random;


  public void Generate(int Seed)
  {
    // Create rng.
    _random = new System.Random(Seed);

    // Generate entities.
    GenerateSun();
    GeneratePlanets();
  }

  private void GenerateSun()
  {
    Sun = CreateInstance<SunModel>();
    Sun.Generate(ref _random);
  }

  private void GeneratePlanets()
  {
    int planetCount = _random.Next(MIN_PLANETS, MAX_PLANETS);
    Planets = new PlanetModel[planetCount];

    float currentDistance = 0;

    for (int i = 0; i < planetCount; i++)
    {
      currentDistance += (float)_random.NextDouble() * (MAX_PLANET_SPACING - MIN_PLANET_SPACING) + MIN_PLANET_SPACING;
      PlanetModel planet = CreateInstance<PlanetModel>();
      planet.Generate(ref _random, currentDistance);
      Planets[i] = planet;
    }
  }
}