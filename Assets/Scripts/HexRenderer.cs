using System.Collections.Generic;
using UnityEngine;

public struct Face
{
  public List<Vector3> Vertices { get; private set; }
  public List<int> Triangles { get; private set; }
  public List<Vector2> UVs { get; private set; }

  public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
  {
    this.Vertices = vertices;
    this.Triangles = triangles;
    this.UVs = uvs;
  }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
  private Mesh _mesh;
  private MeshFilter _meshFilter;
  private MeshRenderer _meshRenderer;

  private List<Face> _faces;

  public Material material;
  public float innerSize = 0.995f;
  public float outerSize = 1.005f;
  public float height = 1.0f;

  public void DrawMesh()
  {
    this.DrawFaces();
    this.CombineFaces();
  }

  public void DrawFaces()
  {
    this._faces = new List<Face>();

    // Top faces.
    for (int point = 0; point < 6; point++)
    {
      this._faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
    }
  }

  protected Vector3 GetPoint(float size, float height, int index)
  {
    float angle = (60 * index) * Mathf.Deg2Rad;
    return new Vector3(size * Mathf.Cos(angle), height, size * Mathf.Sin(angle));
  }

  private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
  {
    Vector3 pointA = GetPoint(innerRad, heightB, point);
    Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
    Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
    Vector3 pointD = GetPoint(outerRad, heightA, point);

    List<Vector3> vertices = new() { pointA, pointB, pointC, pointD };
    List<int> triangles = new() { 0, 1, 2, 2, 3, 0 };
    List<Vector2> uvs = new() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

    if (reverse)
    {
      vertices.Reverse();
    }

    return new Face(vertices, triangles, uvs);
  }

  private void CombineFaces()
  {
    List<Vector3> vertices = new();
    List<int> triangles = new();
    List<Vector2> uvs = new();

    for (int i = 0; i < this._faces.Count; i++)
    {
      // Add vertices and uvs.
      vertices.AddRange(this._faces[i].Vertices);
      uvs.AddRange(this._faces[i].UVs);

      // Offset triangles.
      int offset = (4 * i);
      foreach (int triangle in this._faces[i].Triangles)
      {
        triangles.Add(triangle + offset);
      }
    }

    this._mesh.vertices = vertices.ToArray();
    this._mesh.triangles = triangles.ToArray();
    this._mesh.uv = uvs.ToArray();
    this._mesh.RecalculateNormals();
  }

  private void Awake()
  {
    this._meshFilter = GetComponent<MeshFilter>();
    this._meshRenderer = GetComponent<MeshRenderer>();

    this._mesh = new Mesh
    {
      name = "Hex"
    };

    this._meshFilter.mesh = this._mesh;
    this._meshRenderer.material = this.material;
  }

  private void OnEnable()
  {
    this.DrawMesh();
  }

  private void OnValidate()
  {
    if (Application.isPlaying)
    {
      this.DrawMesh();
    }
  }

}