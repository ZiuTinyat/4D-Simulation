using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    public static class Math4D {
        public static Vector4 Cross4(Vector4 a, Vector4 b, Vector4 c) {
            Vector4 res = Vector4.zero;
            float A, B, C, D, E, F;
            A = (b[0] * c[1]) - (b[1] * c[0]);
            B = (b[0] * c[2]) - (b[2] * c[0]);
            C = (b[0] * c[3]) - (b[3] * c[0]);
            D = (b[1] * c[2]) - (b[2] * c[1]);
            E = (b[1] * c[3]) - (b[3] * c[1]);
            F = (b[2] * c[3]) - (b[3] * c[2]);
            res[0] = (a[1] * F) - (a[2] * E) + (a[3] * D);
            res[1] = -(a[0] * F) + (a[2] * C) - (a[3] * B);
            res[2] = (a[0] * E) - (a[1] * C) + (a[3] * A);
            res[3] = -(a[0] * D) + (a[1] * B) - (a[2] * A);
            return res;
        }
    }
    public class Matrix5x5 { // TODO
        public float    m00, m01, m02, m03, m04,
                        m10, m11, m12, m13, m14,
                        m20, m21, m22, m23, m24,
                        m30, m31, m32, m33, m34,
                        m40, m41, m42, m43, m44;
    }

    public class Vector5 : IEquatable<Vector5> { // TODO
        public float x, y, z, w, v;

        bool IEquatable<Vector5>.Equals(Vector5 other) {
            throw new NotImplementedException();
        }
    }
}
