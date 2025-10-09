using System.Collections.Generic;
using System.Linq;
using Godot;

namespace WarringStates.Map;

public sealed partial class HexagonMesh : MeshInstance3D
{
    private List<Vector3> Vertices { get; } = [];
    private List<int> Indices { get; } = [];

    private SurfaceTool SurfaceTool { get; } = new();
    private int Index { get; set; }
    
    public void Triangulate(ICollection<HexagonCell> cells)
    {
        SurfaceTool.Begin(Mesh.PrimitiveType.Triangles);
        
        foreach (var cell in cells)
        {
            var center = cell.Position;
            AddTriangle(center, center + HexagonMetrics.Corners[0], center + HexagonMetrics.Corners[1]);
        }
        
        // var center = cells.FirstOrDefault().Position;
        // AddTriangle(center, center + HexagonMetrics.Corners[0], center + HexagonMetrics.Corners[1]);
        
        SurfaceTool.GenerateNormals();
        Mesh = SurfaceTool.Commit();
        
//         var st = new SurfaceTool();
//
//         st.Begin(Mesh.PrimitiveType.Triangles);
//
// // Prepare attributes for AddVertex.
//         st.SetNormal(new Vector3(0, 0, 1));
//         st.SetUV(new Vector2(0, 0));
// // Call last for each vertex, adds the above attributes.
//         st.AddVertex(new Vector3(-1, -1, 0));
//
//         st.SetNormal(new Vector3(0, 0, 1));
//         st.SetUV(new Vector2(0, 1));
//         st.AddVertex(new Vector3(-1, 1, 0));
//
//         st.SetNormal(new Vector3(0, 0, 1));
//         st.SetUV(new Vector2(1, 1));
//         st.AddVertex(new Vector3(1, 1, 0));
//
// // Commit to a mesh.
//         st.AddIndex(0);
//         st.AddIndex(1);
//         st.AddIndex(2);
//
//         st.AddIndex(1);
//         st.AddIndex(3);
//         st.AddIndex(2);
//         Mesh = st.Commit();
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        SurfaceTool.AddVertex(v1);
        SurfaceTool.AddVertex(v2);
        SurfaceTool.AddVertex(v3);
        SurfaceTool.AddIndex(Index++);
        SurfaceTool.AddIndex(Index++);
        SurfaceTool.AddIndex(Index++);
    }
}
