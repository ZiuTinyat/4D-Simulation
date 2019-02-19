using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    [RequireComponent(typeof(Transform4D))]
    [RequireComponent(typeof(Camera))]
    public class Camera4D : MonoBehaviour {

        private Transform4D _transform4D;
        public Transform4D transform4D { get { if (!_transform4D) _transform4D = GetComponent<Transform4D>(); return _transform4D; } }

        public ProjectionMode projection4D = ProjectionMode.Perspective;

        public Vector4 ToEye(Vector4 p) {
            return transform4D.worldToLocalMatrix * (p - transform4D.localPosition);
        }

        public Vector4 Project(Vector4 p) {
            switch (projection4D) {
                case ProjectionMode.Othogonal:
                    return p;
                case ProjectionMode.Perspective:
                    if (p.w == 0) return Vector4.zero;
                    else return new Vector4(p.x / p.w, p.y / p.w, p.z / p.w, p.w);
                case ProjectionMode.Sliced:
                    return Vector4.zero; // TODO
                default: return Vector4.zero;
            }
        }

        public Vector4 ToWorld(Vector4 p) {
            return transform4D.localToWorldMatrix * p + transform4D.localPosition;
        }
    }
}
