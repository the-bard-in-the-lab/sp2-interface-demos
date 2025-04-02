using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GraphGenerator : MonoBehaviour
{
    public double xMin = 0f;
    public double xMax = 100f;
    public double yMin = 0f;
    public double yMax = 50f;    
    private RectTransform myRectTransform;
    private LineRendererUI myRenderer;
    public float lineWidth = 2f;
    public Slider widthController;
    public List<Vector2> myCoords = new List<Vector2>{
            new Vector2(0, 0),
            new Vector2(100, 100),
            new Vector2(200, 150),
            new Vector2(500, 300),
        };

    // Start is called before the first frame update
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        myRenderer = GetComponent<LineRendererUI>();
        widthController.value = lineWidth;
    }
    public void UpdateWidth() {
        lineWidth = widthController.value;
        myRenderer.SetLineWidth(lineWidth);
    }

    public void DrawGraph(List<Vector2> points) {
        myCoords = points;
        
        double xScalar = myRectTransform.rect.width / (xMax - xMin);
        double yScalar = myRectTransform.rect.height / (yMax - yMin);

        double xShift = xMin;
        double yShift = yMin;

        List<Vector2> pointsConverted = new List<Vector2>();

        for (int i = 0; i < points.Count; i ++) {
            pointsConverted.Add(new Vector2((float) ((points[i].x - xShift) * xScalar),
                                            (float) ((points[i].y - yShift) * yScalar)));
        }
        
        //pointsConverted.Insert(0, new Vector2(pointsConverted[0].x, 0));
        myRenderer.SetVertices(pointsConverted);
        
        // Culls points on 2D graphs that pass the vertical line test:

        // int startInd = points.Count - 1;
        // int endInd = 0;
        // while (points[startInd].x >= xMin && startInd > 0) {
        //     startInd --;
        // }
        // while (points[endInd].x <= xMax && endInd < points.Count - 1) {
        //     endInd ++;
        // }


        // if (startInd < endInd) {
        //     List<Vector2> pointsCulled = pointsConverted.GetRange(startInd, endInd - startInd);
        //     myRenderer.SetVertices(pointsCulled);
        // }
        // else {
        //     myRenderer.SetVertices(new List<Vector2>());
        // }
    }
}
