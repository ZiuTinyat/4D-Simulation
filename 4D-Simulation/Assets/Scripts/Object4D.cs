using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    [RequireComponent(typeof(Transform4D))]
    public class Object4D : MonoBehaviour {
        // Transform
        Transform4D _t;
        public Transform4D transform4D { get { if (!_t) _t = GetComponent<Transform4D>(); return _t; } }
        // Model
        [SerializeField] TextAsset modelFile;
        public Model4D model;

        void Awake() {
            model = Model4D.FromJson(modelFile.text);
            model.Init();
        }
    }
}

