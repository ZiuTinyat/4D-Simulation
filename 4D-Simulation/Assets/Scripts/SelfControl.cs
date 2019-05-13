using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FourthDimension;

public class SelfControl : MonoBehaviour
{
    public Transform4D transform4D;

    // Start is called before the first frame update
    void Start()
    {
        transform4D = GetComponent<Transform4D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform4D.rot_w += new Vector3(15f, 23f, 49f) * Time.deltaTime;
    }
}
