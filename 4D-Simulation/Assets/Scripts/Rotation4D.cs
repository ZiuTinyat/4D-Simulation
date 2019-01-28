using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperSpace {
    [Serializable]
    public struct Rotation4D : IEquatable<Rotation4D> {

        public float xy, xz, xw, yz, yw, zw;

        public Rotation4D(float xy, float xz, float xw, float yz, float yw, float zw) {
            this.xy = xy;
            this.xz = xz;
            this.xw = xw;
            this.yz = yz;
            this.yw = yw;
            this.zw = zw;
        }

        public bool Equals(Rotation4D other) {
            throw new NotImplementedException();
        }
        
    }
}
