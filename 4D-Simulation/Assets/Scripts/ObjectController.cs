using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FourthDimension;
public class ObjectController : MonoBehaviour
{
    [SerializeField] GameObject MagicCircle;
    private Transform follow;

    public static bool showOrtho = false, showSliced = true;
    public static ObjectController objectInControl = null;
    public static ObjectController objectMoving = null;
    public static void Control(ObjectController obj, Transform follow) {
        obj.Control(follow);
    }
    public static void CancelControl() {
        if (objectInControl) objectInControl.RemoveControl();
    }
    public static void Move(ObjectController obj, Transform follow) {
        obj.Move(follow);
    }
    public static void CancelMove() {
        if (objectMoving) objectMoving.RemoveMove();
    }
    public static void RotateControl(Vector3 axis, float deg) {
        if (objectInControl) objectInControl.Rotate(axis, deg);
        else { // Move Manager
            float value = Manager4D.instance.transform4D.localPosition.w;
            value = Mathf.Clamp(value + deg / 360f / 5f, -0.5f, 0.5f);
            Manager4D.instance.transform4D.localPosition = new Vector4(0, 0, 0, value);
            SlideBarCtrl.SetValue(2f * value);
        }
    }

    public bool inControl = false;
    public bool moving = false;

    Transform4D _t;
    public Transform4D transform4D { get { if (!_t) _t = GetComponent<Transform4D>(); return _t; } }

    public void Control(Transform follow) {
        if (!inControl) {
            CancelControl();
            this.follow = follow;
            inControl = true;
            objectInControl = this;
            StartCoroutine(CircleSizeCoroutine(true));
        }
    }

    public void RemoveControl() {
        if (objectInControl == this) {
            objectInControl = null;
            inControl = false;
            StartCoroutine(CircleSizeCoroutine(false));
        }
    }

    public void Move(Transform follow) {
        if (!moving) {
            CancelMove();
            transform.SetParent(follow);
            moving = true;
            objectMoving = this;
        }
    }

    public void RemoveMove() {
        if (objectMoving == this) {
            transform.SetParent(null);
            objectMoving = null;
            moving = false;
        }
    }

    public void Rotate(Vector3 axis, float deg) {
        deg *= 1f;
        MagicCircle.transform.GetChild(0).localEulerAngles += Vector3.forward * deg;
        transform4D.rot_w += axis * deg;
    }

    IEnumerator CircleSizeCoroutine(bool up) {

        if (up) MagicCircle.SetActive(true);

        float max = 0.2f, min = 0f, dur = 0.5f, t = 0f;
        while (t < dur) {
            float scale = 0f;
            if (up) scale = Mathf.Lerp(min, max, t / dur);
            else scale = Mathf.Lerp(max, min, t / dur);

            MagicCircle.transform.localScale = Vector3.one * scale;
            t += Time.deltaTime;
            yield return null;
        }
        if (up) MagicCircle.transform.localScale = Vector3.one * max;
        else MagicCircle.transform.localScale = Vector3.one * min;

        if (!up)
            MagicCircle.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inControl) {
            //MagicCircle.transform.rotation = follow.rotation * Quaternion.Euler(-90f, 0, 0);
            MagicCircle.transform.LookAt(MagicCircle.transform.position + follow.up);
            //MagicCircle.transform.GetChild(0).localEulerAngles += Vector3.forward * Time.deltaTime * 10f;
            Rotate(MagicCircle.transform.up, 10f * Time.deltaTime);
        }
    }
}
