using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FourthDimension;

[RequireComponent(typeof(Hand))]
public class HandInteraction : MonoBehaviour
{
    Hand _h;
    public Hand hand { get { if (!_h) _h = GetComponent<Hand>(); return _h; } }
    
    public ObjectController touchedObject;
    public ObjectController grabbedObject;

    private bool isScrolling = false;
    private float scrollAnchor = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleUpdate();
        SelectUpdate();
        MoveUpdate();
        ScrollUpdate();
    }

    private void ToggleUpdate() {
        if (hand.GetMenuDown()) {
            if (!ObjectController.showOrtho && ObjectController.showSliced) {
                ObjectController.showOrtho = true;
            } else if (ObjectController.showOrtho && ObjectController.showSliced) {
                ObjectController.showSliced = false;
            } else {
                ObjectController.showOrtho = false;
                ObjectController.showSliced = true;
            }
        }
    }

    private void SelectUpdate() {
        if (hand.GetTriggerDown()) {
            if (touchedObject) {
                ObjectController.Control(touchedObject, transform);
            } else {
                ObjectController.CancelControl();
            }
        }
    }

    private void MoveUpdate() {
        if (hand.GetGripDown()) {
            if (touchedObject) {                
                ObjectController.Move(touchedObject, transform);
            } 
        }
        if (hand.GetGripUp()) {
            ObjectController.CancelMove();
        }
    }

    private void ScrollUpdate() {
        if (hand.GetTouchPadTouch()) { // Touching
            Vector2 axis = hand.GetTouchPadAxis();
            if (!isScrolling) { // Touch down
                if (axis.sqrMagnitude > 0.3f * 0.3f) {
                    isScrolling = true;
                    scrollAnchor = Mathf.Atan2(axis.x, axis.y);
                }
            } else { // Touch update
                if (axis.sqrMagnitude > 0f) { // Skip (0, 0) case
                    float currentAnchor = Mathf.Atan2(axis.x, axis.y);
                    float diff = currentAnchor - scrollAnchor;
                    if (diff > Mathf.PI) diff -= 2f * Mathf.PI;
                    else if (diff < -Mathf.PI) diff += 2f * Mathf.PI;

                    ObjectController.RotateControl(transform.up, diff * Mathf.Rad2Deg);
                    scrollAnchor = currentAnchor;
                    /*if (diff > scrollThres) { // Next module
                        moduleSelectIndex++;
                        scrollAnchor = currentAnchor;
                        AudioPlayer.PlaySFX(AudioPlayer.instance.TouchPadSwap);
                        hand.TriggerHapticPulse(500);
                    } else if (diff < -scrollThres) { // Last module
                        moduleSelectIndex--;
                        scrollAnchor = currentAnchor;
                        AudioPlayer.PlaySFX(AudioPlayer.instance.TouchPadSwap);
                        hand.TriggerHapticPulse(500);
                    }*/
                }
            }
        } else { // Not touching
            if (isScrolling) { // End scrolling
                isScrolling = false;
                scrollAnchor = 0f;
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        var obj = other.GetComponent<ObjectController>();
        if (obj) {
            touchedObject = obj;
        }
    }

    private void OnTriggerExit(Collider other) {
        var obj = other.GetComponent<ObjectController>();
        if (obj) {
            if (touchedObject == obj)
                touchedObject = null;
        }
    }
}
