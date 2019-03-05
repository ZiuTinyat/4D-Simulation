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
        
        public Vector4 ProjectTo3D(Vector4 worldPos) {
            // To local
            Vector4 local = transform4D.WorldToLocalPosition(worldPos);

            // Project
            Vector4 projected = Vector4.zero;
            switch (projection4D) {
                case ProjectionMode.Othogonal:
                    projected = local;
                    break;
                case ProjectionMode.Perspective:
                    if (local.w != 0)
                        projected = new Vector4(local.x / local.w, local.y / local.w, local.z / local.w, local.w);
                    break;
                case ProjectionMode.Sliced:
                    // TODO
                    break;
                default: break;
            }

            // To world
            return transform4D.LocalToWorldPosition(projected);
        }
    }
}
