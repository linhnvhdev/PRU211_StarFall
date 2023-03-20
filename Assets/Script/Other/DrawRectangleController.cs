using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRectangleController : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 4;
    public float range = 3;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.startWidth = 0.1f;
        line.endWidth = 0.11f;
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.useWorldSpace = true;
        Vector3[] points = new Vector3[5];
        Debug.Log("Draw Rectangle around");
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("Laser width:" +width);
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Laser height:" + height);
        Vector3 center = transform.position;
        Debug.Log("Laser center:" + center);

        points[0] = new Vector3(center.x - (float)(range + 0.5), center.y - height );
        points[1] = new Vector3(center.x - (float)(range + 0.5), center.y + height );
        points[2] = new Vector3(center.x + (float)(range + 0.5), center.y + height );
        points[3] = new Vector3(center.x + (float)(range + 0.5), center.y - height );
        points[4] = points[0];
        Debug.Log("Point 0:" + points[0]);
        Debug.Log("Point 1:" + points[1]);
        Debug.Log("Point 2:" + points[2]);
        Debug.Log("Point 3:" + points[3]);
        Debug.Log("Point 4:" + points[4]);
        line.SetPositions(points);
    }

}
