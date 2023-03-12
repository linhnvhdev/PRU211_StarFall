using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleController : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    public float range = 2;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * range;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * range;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}
