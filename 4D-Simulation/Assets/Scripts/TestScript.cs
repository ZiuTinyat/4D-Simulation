using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FourthDimension;

public class TestScript : MonoBehaviour
{
    Transform4D t { get { return GetComponent<Transform4D>(); } }

    public Vector4[] shapeVerteces = {
            new Vector4 ( 1,  1,  1,  1),
            new Vector4 ( 1,  1,  1, -1),
            new Vector4 ( 1,  1, -1,  1),
            new Vector4 ( 1,  1, -1, -1),
            new Vector4 ( 1, -1,  1,  1),
            new Vector4 ( 1, -1,  1, -1),
            new Vector4 ( 1, -1, -1,  1),
            new Vector4 ( 1, -1, -1, -1),
            new Vector4 (-1,  1,  1,  1),
            new Vector4 (-1,  1,  1, -1),
            new Vector4 (-1,  1, -1,  1),
            new Vector4 (-1,  1, -1, -1),
            new Vector4 (-1, -1,  1,  1),
            new Vector4 (-1, -1,  1, -1),
            new Vector4 (-1, -1, -1,  1),
            new Vector4 (-1, -1, -1, -1)
        };
    public Vector2Int[] shapeEdges = {
            new Vector2Int( 0,  1), new Vector2Int( 0,  2), new Vector2Int( 0,  4), new Vector2Int( 0,  8),
            new Vector2Int( 1,  3), new Vector2Int( 1,  5), new Vector2Int( 1,  9),
            new Vector2Int( 2,  3), new Vector2Int( 2,  6), new Vector2Int( 2, 10),
            new Vector2Int( 3,  7), new Vector2Int( 3, 11),
            new Vector2Int( 4,  5), new Vector2Int( 4,  6), new Vector2Int( 4, 12),
            new Vector2Int( 5,  7), new Vector2Int( 5, 13),
            new Vector2Int( 6,  7), new Vector2Int( 6, 14),
            new Vector2Int( 7, 15),
            new Vector2Int( 8,  9), new Vector2Int( 8, 10), new Vector2Int( 8, 12),
            new Vector2Int( 9, 11), new Vector2Int( 9, 13),
            new Vector2Int(10, 11), new Vector2Int(10, 14),
            new Vector2Int(11, 15),
            new Vector2Int(12, 13), new Vector2Int(12, 14),
            new Vector2Int(13, 15),
            new Vector2Int(14, 15)
        };

    public Vector4[] visualVerteces;

    Camera4D c4;

    // Start is called before the first frame update
    void Start()
    {
        c4 = Camera.main.GetComponent<Camera4D>();
        visualVerteces = new Vector4[shapeVerteces.Length];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(c4.ToWorld(c4.ToEye(new Vector4(2, 3, 4, 5))));
        for (int i = 0; i < shapeVerteces.Length; i++) {
            visualVerteces[i] = c4.ToWorld(c4.Project(c4.ToEye(shapeVerteces[i])));
        }
        foreach (var edge in shapeEdges) {
            Debug.DrawLine(visualVerteces[edge.x], visualVerteces[edge.y], Color.red);
        }
    }
}
