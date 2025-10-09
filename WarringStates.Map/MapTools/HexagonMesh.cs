using System.Collections.Generic;
using Godot;

namespace WarringStates.Map;

public sealed partial class HexagonMesh : MeshInstance3D
{
    private List<Vector3> Vertices { get; } = [];
    private List<int> Indices { get; } = [];

    public void Triangulate(ICollection<HexagonCell> cells)
    {
        Vertices.Clear();
        Indices.Clear();
        foreach (var cell in cells)
        {
            var center = cell.Position;
            AddTriangle(
                center,
                center + HexagonMetrics.Corners[0],
                center + HexagonMetrics.Corners[1]
            );
        }
        UpdateMesh();
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var index = Vertices.Count;
        Vertices.Add(v1);
        Vertices.Add(v2);
        Vertices.Add(v3);
        Indices.Add(index);
        Indices.Add(index + 1);
        Indices.Add(index + 2);
    }

    private void UpdateMesh()
    {
        using var surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)Mesh.ArrayType.Max);
        surfaceArray[(int)Mesh.ArrayType.Vertex] = Vertices.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = Indices.ToArray();
        var surfaceTool = new SurfaceTool();
        surfaceTool.CreateFromArrays(surfaceArray);
        surfaceTool.GenerateNormals();
        Mesh = surfaceTool.Commit();
        surfaceTool.Free();
    }
}
