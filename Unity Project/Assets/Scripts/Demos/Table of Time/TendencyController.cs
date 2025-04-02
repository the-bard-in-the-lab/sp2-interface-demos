using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TendencyController : MonoBehaviour
{
    List<double> recentHits;
    private Vector3 referenceVel = Vector3.zero;
    public Gradient myColor;
    public float variance = 4f;
    public int avg_count = 5;
    [SerializeField] Light2D myLight;

    void Start()
    {
        recentHits = new List<double>();
        recentHits.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        while (recentHits.Count > avg_count) {
            recentHits.RemoveAt(0);
        }
        if (recentHits.Count > 0) {
            Vector3 targetPos = new Vector3( (float) recentHits.Average() * variance, 1, 0);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref referenceVel, 0.1f);
            Color thisColor = myColor.Evaluate(Math.Abs(transform.position.x) / (variance / 2f));
            //GetComponent<SpriteRenderer>().color = thisColor;
            myLight.color = thisColor;
        }
    }

    public void newHit(double tendency) {
        recentHits.Add(tendency);
    }
}
