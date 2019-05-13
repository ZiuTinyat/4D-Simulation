using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBarCtrl : MonoBehaviour
{
    public static SlideBarCtrl instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public static void SetValue(float frac) {
        float max = 2.5f;
        Vector3 newP = instance.transform.localPosition;
        newP.z = frac * max;
        instance.transform.localPosition = newP;
    }
}
