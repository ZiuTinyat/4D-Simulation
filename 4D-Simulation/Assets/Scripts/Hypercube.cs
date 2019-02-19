using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
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

        public ProjectionMode projectionMode;

        public Rotation4D rotation;

        public Vector4[] visualVerteces;

        public Vector4 Rotate4D(Vector4 v, Rotation4D r) {
            Matrix4x4 rotMat = Matrix4x4.identity;
            { // xy
                float sin = Mathf.Sin(rotation.xy * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotation.xy * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, sin, 0, 0), new Vector4(-sin, cos, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
                rotMat = m * rotMat;
            }
            { // yz
                float sin = Mathf.Sin(rotation.yz * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotation.yz * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, sin, 0), new Vector4(0, sin, -cos, 0), new Vector4(0, 0, 0, 1));
                rotMat = m * rotMat;
            }
            { // xz
                float sin = Mathf.Sin(rotation.zx * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotation.zx * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, -sin, 0), new Vector4(0, 1, 0, 0), new Vector4(sin, 0, cos, 0), new Vector4(0, 0, 0, 1));
                rotMat = m * rotMat;
            }
            { // xw
                float sin = Mathf.Sin(rotation.xw * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotation.xw * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, 0, -sin), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(sin, 0, 0, cos));
                rotMat = m * rotMat;
            }
            { // yw
                float sin = Mathf.Sin(rotation.yw * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotation.yw * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, 0, -sin), new Vector4(0, 0, 1, 0), new Vector4(0, sin, 0, cos));
                rotMat = m * rotMat;
            }
            { // zw
                float sin = Mathf.Sin(rotation.zw * Mathf.Deg2Rad);
                float cos = Mathf.Cos(rotation.zw * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, cos, -sin), new Vector4(0, 0, sin, cos));
                rotMat = m * rotMat;
            }
            return rotMat * v;
        }

        // Start is called before the first frame update
        void Start() {
            visualVerteces = new Vector4[shapeVerteces.Length];
            shapeVerteces.CopyTo(visualVerteces, 0);
            rotation = new Rotation4D();
        }

        // Update is called once per frame
        void Update() {
            //rotation.xw += Time.deltaTime * 1f;
            //rotation.xz += Time.deltaTime * 1.5f;
            //rotation.yw += Time.deltaTime * 2f;
            for (int i = 0; i < shapeVerteces.Length; i++) {
                visualVerteces[i] = Rotate4D(shapeVerteces[i], rotation);
            }
            foreach (var edge in shapeEdges) {
                Debug.DrawLine(ProjectTo3D(visualVerteces[edge.x]), ProjectTo3D(visualVerteces[edge.y]), Color.red);
            }
        }

        private Vector3 ProjectTo3D(Vector4 v4) {
            switch (projectionMode) {
                case ProjectionMode.Othogonal:
                    return v4;
                case ProjectionMode.Perspective:
                    return (v4) / (v4.w + 5f);
                default:
                    return Vector4.zero;
            }
        }
    }
}
