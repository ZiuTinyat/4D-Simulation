using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    [Serializable]
    public struct Rotation4D : IEquatable<Rotation4D> { // 3D equivalent: Vector3 (Euler) 

        public float yz, zx, xy, xw, yw, zw; // Euler, in degree

        // Constructors
        public Rotation4D(float yz = 0, float zx = 0, float xy = 0, float xw = 0, float yw = 0, float zw = 0) {
            this.yz = yz; this.zx = zx; this.xy = xy;
            this.xw = xw; this.yw = yw; this.zw = zw;
        }
        public Rotation4D(Vector3 euler3D, float xw = 0, float yw = 0, float zw = 0) {
            yz = euler3D.x; zx = euler3D.y; xy = euler3D.z;
            this.xw = xw; this.yw = yw; this.zw = zw;
        }
        public Rotation4D(Vector3 euler3D, Vector3 eulerW) {
            yz = euler3D.x; zx = euler3D.y; xy = euler3D.z;
            xw = eulerW.x; yw = eulerW.y; zw = eulerW.z;
        }

        // Static 
        public static Rotation4D identity { get { return new Rotation4D(); } }

        // Method
        public Vector3 ToEuler3D() { return new Vector3(yz, zx, xy); }
        public Vector3 ToEulerW() { return new Vector3(xw, yw, zw); }
        public Quaternion ToQuaternion() { return Quaternion.Euler(ToEuler3D()); }
        public Matrix4x4 ToMatrix() { // TODO Rotor
            Matrix4x4 res = Matrix4x4.identity;
            { // xy --> z
                float sin = Mathf.Sin(xy * Mathf.Deg2Rad);
                float cos = Mathf.Cos(xy * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, sin, 0, 0), new Vector4(-sin, cos, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
                res = m * res;
            }
            { // yz --> x
                float sin = Mathf.Sin(yz * Mathf.Deg2Rad);
                float cos = Mathf.Cos(yz * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, sin, 0), new Vector4(0, sin, -cos, 0), new Vector4(0, 0, 0, 1));
                res = m * res;
            }
            { // xz --> y
                float sin = Mathf.Sin(zx * Mathf.Deg2Rad);
                float cos = Mathf.Cos(zx * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, -sin, 0), new Vector4(0, 1, 0, 0), new Vector4(sin, 0, cos, 0), new Vector4(0, 0, 0, 1));
                res = m * res;
            }
            { // xw
                float sin = Mathf.Sin(xw * Mathf.Deg2Rad);
                float cos = Mathf.Cos(xw * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, 0, -sin), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(sin, 0, 0, cos));
                res = m * res;
            }
            { // yw
                float sin = Mathf.Sin(yw * Mathf.Deg2Rad);
                float cos = Mathf.Cos(yw * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, 0, -sin), new Vector4(0, 0, 1, 0), new Vector4(0, sin, 0, cos));
                res = m * res;
            }
            { // zw
                float sin = Mathf.Sin(zw * Mathf.Deg2Rad);
                float cos = Mathf.Cos(zw * Mathf.Deg2Rad);
                Matrix4x4 m = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, cos, -sin), new Vector4(0, 0, sin, cos));
                res = m * res;
            }
            return res;
        }
        public void Trim() {
            yz = yz % 360f; zx = zx % 360f; xy = xy % 360f;
            xw = xw % 360f; yw = yw % 360f; zw = zw % 360f;
        }

        // Override
        public bool Equals(Rotation4D other) {
            Trim();
            other.Trim();
            return (xy == other.xy && zx == other.zx && xw == other.xw && yz == other.yz && yw == other.yw && zw == other.zw);
        }
        
    }
}
