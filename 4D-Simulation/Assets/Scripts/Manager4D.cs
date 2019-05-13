using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    // The transform of my 3D world in 4D space
    [RequireComponent(typeof(Transform4D))]
    public class Manager4D : MonoBehaviour {
        // Singleton
        private static Manager4D _i;
        public static Manager4D instance { get { if (!_i) _i = FindObjectOfType(typeof(Manager4D)) as Manager4D; Debug.Assert(_i); return _i; } }

        private Transform4D _t4;
        public Transform4D transform4D { get { if (!_t4) _t4 = GetComponent<Transform4D>(); return _t4; } }
        

        public Vector4 PositionMyWorldTo4D(Vector3 pos) {
            return transform4D.LocalToWorldPosition(pos);
        }

        public Vector4 Position4DToMyWorld(Vector4 pos) {
            return transform4D.WorldToLocalPosition(pos);
        }

        public Vector3 ProjectToMyWorld(Vector4 worldPos, ProjectionMode mode) {
            switch (mode) {
                case ProjectionMode.Orthogonal:
                    return Position4DToMyWorld(worldPos);
                default:
                    return Vector3.zero;
            }
        }

        public bool SliceEdge(Vector4 worldPos1, Vector4 worldPos2, out Vector4 localPosSliced) {
            localPosSliced = Vector4.zero;
            Vector4 myPos1 = transform4D.LocalToWorldPosition(worldPos1);
            Vector4 myPos2 = transform4D.LocalToWorldPosition(worldPos2);
            float n = myPos2.w - myPos1.w;
            if (n != 0f) {
                float t = -myPos1.w / n;
                if (t > 1f || t < 0f) {
                    return false;
                } else {
                    localPosSliced = myPos1 + t * (myPos2 - myPos1);
                    return true;
                }
            } else { // Singularity
                return false;
            }
        }

        public bool SliceFace(List<Vector4> worldPoses, out List<Vector4> worldPosSliceds) {
            worldPosSliceds = new List<Vector4>();
            int edges = worldPoses.Count;
            worldPoses.Add(worldPoses[0]); // loop
            for (int i = 0; i < edges; i++) {
                Vector4 worldSliced;
                if (SliceEdge(worldPoses[i], worldPoses[i + 1], out worldSliced)) {
                    worldPosSliceds.Add(worldSliced);
                }
            }
            return (worldPosSliceds.Count > 0);
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
