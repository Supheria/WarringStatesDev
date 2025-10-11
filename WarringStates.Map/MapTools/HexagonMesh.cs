using System;
using System.Collections.Generic;
using Godot;

namespace WarringStates.Map;

public partial class HexagonMesh : MeshInstance3D
{
    public event CollisionObject3D.InputEventEventHandler? UnhandledInputEvent;
    private List<Vector3> Vertices { get; } = [];
    private List<Vector3> Normals { get; } = [];
    private List<int> Indices { get; } = [];
    private CollisionShape3D? CollisionShape { get; set; }

    public override void _Ready()
    {
        CollisionShape = GetNode<CollisionShape3D>("Collision/CollisionShape");
    }

    private void CollisionOnInputEvent(
        Node camera,
        InputEvent @event,
        Vector3 eventPosition,
        Vector3 normal,
        long shapeIdx
    )
    {
        UnhandledInputEvent?.Invoke(camera, @event, eventPosition, normal, shapeIdx);
    }

    public void CreateCollision()
    {
        if (CollisionShape is null)
            throw new NullReferenceException();
        CollisionShape.Shape = Mesh.CreateTrimeshShape();
    }

    public void Triangulate(ICollection<HexagonCell> cells)
    {
        Vertices.Clear();
        Indices.Clear();
        Normals.Clear();
        foreach (var cell in cells)
        {
            var center = cell.Position;
            for (var i = 0; i < 6; i++)
            {
                AddTriangle(
                    center,
                    center + HexagonMetrics.Corners[i],
                    center + HexagonMetrics.Corners[i + 1]
                );
            }
        }
        UpdateMesh();
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var index = Vertices.Count;
        Indices.Add(index);
        Indices.Add(index + 1);
        Indices.Add(index + 2);
        Vertices.Add(v1);
        Vertices.Add(v2);
        Vertices.Add(v3);
        var normal = GetTriangleNormal(v1, v2, v3);
        Normals.Add(normal);
        Normals.Add(normal);
        Normals.Add(normal);
    }

    private static Vector3 GetTriangleNormal(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var side1 = v2 - v1;
        var side2 = v3 - v1;
        var normal = side2.Cross(side1);
        return normal;
    }

    private void UpdateMesh()
    {
        using var surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)Mesh.ArrayType.Max);
        surfaceArray[(int)Mesh.ArrayType.Vertex] = Vertices.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Normal] = Normals.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = Indices.ToArray();
        var mesh = new ArrayMesh();
        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, surfaceArray);
        Mesh = mesh;
    }
}
