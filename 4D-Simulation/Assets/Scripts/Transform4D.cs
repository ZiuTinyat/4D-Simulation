using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension { 
    [RequireComponent(typeof(Transform))]
    public class Transform4D : MonoBehaviour { // 3D equivalent: Transform

        // TODO global pos and rot and parenting
        // TODO scale..?

        // Additional local position
        public float pos_w;
        // Additional local rotation
        public Vector3 rot_w;

        // Position
        //public Vector4 position { get { return GetLocalPosition; } set { SetPosition(value); } }
        public Vector4 localPosition { get { return GetLocalPosition(); } set { SetLocalPosition(value); } }
        private Vector4 GetLocalPosition() { return ((Vector4) transform.localPosition + new Vector4(0, 0, 0, pos_w)); }
        private void SetLocalPosition(Vector4 p) { transform.localPosition = p; pos_w = p.w; }

        // Rotation
        //public Rotation4D rotation { get { return GetRotation(); } set { SetRotation(value); } }
        public Rotation4D localRotaion { get { return GetLocalRotation(); } set { SetLocalRotation(value); } }
        private Rotation4D GetLocalRotation() { return new Rotation4D(transform.localEulerAngles, rot_w); }
        private void SetLocalRotation(Rotation4D r) { transform.localEulerAngles = r.ToEuler3D(); rot_w = r.ToEulerW(); }

        // Info
        public Vector4 up { get { return localToWorldMatrix * new Vector4(0, 1, 0, 0); } }
        public Vector4 forward { get { return localToWorldMatrix * new Vector4(0, 0, 1, 0); } }
        public Vector4 right { get { return localToWorldMatrix * new Vector4(1, 0, 0, 0); } }
        public Vector4 inward { get { return localToWorldMatrix * new Vector4(0, 0, 0, 1); } }
        public Matrix4x4 localToWorldMatrix { get { return localRotaion.ToMatrix(); } } // TODO consider translation, 5x5 matrix, dirty
        public Matrix4x4 worldToLocalMatrix { get { return localToWorldMatrix.transpose; } } // TODO same


        // Methods
        public void Translate(Vector4 p, Space relativeTo = Space.Self) {
            // TODO world translate and parenting
            transform.Translate(p, relativeTo);
            pos_w += p.w;
        }
        public void LookAt(Vector4 worldPosition) {
            // TODO world
            //Vector4 dir = (worldPosition - localPosition).normalized;
            //transform.LookAt();
        }
    }
}
