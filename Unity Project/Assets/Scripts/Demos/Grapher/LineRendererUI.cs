using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererUI : MaskableGraphic
{
    public float lineWidth = 2f;
    private List<Vector2> vertices;
    
    public List<Vector2> GetVertices() {
        return vertices;
    }
    public void SetVertices(List<Vector2> newVertices) {
        vertices = newVertices;
        UpdateGeometry();
    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (vertices != null) {
            if (vertices.Count > 1) {
                for (int i = 1; i < vertices.Count; i ++) {
                    UIVertex vertex = UIVertex.simpleVert;
                    Vector2 myDir = (vertices[i] - vertices[i - 1]).normalized;
                    Vector2 perpendicular = new Vector2(-myDir.y, myDir.x); // Rotates 90 degrees counterclockwise
                    
                    vertex.position = vertices[i-1] + perpendicular * 0.5f * lineWidth;
                    vh.AddVert(vertex);

                    vertex.position = vertices[i-1] - perpendicular * 0.5f * lineWidth;
                    vh.AddVert(vertex);
                    
                    vertex.position = vertices[i] + perpendicular * 0.5f * lineWidth;
                    vh.AddVert(vertex);
                    
                    vertex.position = vertices[i] - perpendicular * 0.5f * lineWidth;
                    vh.AddVert(vertex);

                    int startInd = (i - 1) * 4;

                    vh.AddTriangle(startInd, startInd + 1, startInd + 2);
                    vh.AddTriangle(startInd + 1, startInd + 2, startInd + 3);
                }
            }
        }
    }
}