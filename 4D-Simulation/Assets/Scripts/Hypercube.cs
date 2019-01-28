using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperSpace {
    public class Hypercube : MonoBehaviour {

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

        public Rotation4D rotation;

        public Vector4[] visualVerteces;

        public Vector4 Rotate4D(Vector4 v, Rotation4D r) {
            Vector4 res = v;
            { // xy
                float sin = Mathf.Sin(rotation.xy);
                float cos = Mathf.Cos(rotation.xy);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, -sin, 0, 0), new Vector4(sin, cos, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
                res = m * res;
            }
            { // xz
                float sin = Mathf.Sin(rotation.xz);
                float cos = Mathf.Cos(rotation.xz);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, -sin, 0), new Vector4(0, 1, 0, 0), new Vector4(sin, 0, cos, 0), new Vector4(0, 0, 0, 1));
                res = m * res;
            }
            { // xw
                float sin = Mathf.Sin(rotation.xw);
                float cos = Mathf.Cos(rotation.xw);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, 0, -sin), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(sin, 0, 0, cos));
                res = m * res;
            }
            { // yz
                float sin = Mathf.Sin(rotation.yz);
                float cos = Mathf.Cos(rotation.yz);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, -sin, 0), new Vector4(0, sin, cos, 0), new Vector4(0, 0, 0, 1));
                res = m * res;
            }
            { // yw
                float sin = Mathf.Sin(rotation.yw);
                float cos = Mathf.Cos(rotation.yw);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, 0, -sin), new Vector4(0, 0, 1, 0), new Vector4(0, sin, 0, cos));
                res = m * res;
            }
            { // zw
                float sin = Mathf.Sin(rotation.zw);
                float cos = Mathf.Cos(rotation.zw);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, cos, -sin), new Vector4(0, 0, sin, cos));
                res = m * res;
            }
            return res;
        }

        // Start is called before the first frame update
        void Start() {
            visualVerteces = new Vector4[shapeVerteces.Length];
            shapeVerteces.CopyTo(visualVerteces, 0);
            rotation = new Rotation4D();
        }

        // Update is called once per frame
        void Update() {
            rotation.xw += Time.deltaTime * 1f;
            rotation.xz += Time.deltaTime * 1.5f;
            rotation.yw += Time.deltaTime * 2f;
            for (int i = 0; i < shapeVerteces.Length; i++) {
                visualVerteces[i] = Rotate4D(shapeVerteces[i], rotation);
            }
            foreach (var edge in shapeEdges) {
                Debug.DrawLine(visualVerteces[edge.x], visualVerteces[edge.y], Color.red);
            }
        }
    }
}
