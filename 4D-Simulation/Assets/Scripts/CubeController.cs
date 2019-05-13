using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FourthDimension;

public class CubeController : MonoBehaviour
{
    private Transform4D _t;
    public Transform4D transform4D { get { if (!_t) _t = GetComponent<Transform4D>(); return _t; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation4D r4 = transform4D.localRotaion;
        r4.xw += Time.deltaTime * 33.36f;
        r4.yw += Time.deltaTime * 42.12f;
        r4.zw += Time.deltaTime * 61.88f;
        transform4D.localRotaion = r4;
    }
}
