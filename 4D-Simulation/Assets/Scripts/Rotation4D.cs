using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperSpace {
    public struct Rotation4D : IEquatable<Rotation4D> {

        public float xy, xz, xw, yz, yw, zw;

        public bool Equals(Rotation4D other) {
            throw new NotImplementedException();
        }
        
    }
}
