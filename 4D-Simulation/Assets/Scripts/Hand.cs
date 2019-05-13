using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class Hand : MonoBehaviour {

    //[SerializeField] GameObject handModel;

    //public HandType handType = HandType.Both;
    public Hand otherHand;

    public SteamVR_Controller.Device controller { get; private set; }

    public bool IsReady() {
        return controller != null;
    }

    // Get input
    public bool GetTriggerDown() {
        return IsReady() && controller.GetHairTriggerDown();
    }
    public bool GetTrigger() {
        return IsReady() && controller.GetHairTrigger();
    }
    public bool GetTrigger(out float axis) {
        axis = IsReady()? controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x : 0f;
        return IsReady() && controller.GetHairTrigger();
    }
    public bool GetTriggerUp() {
        return IsReady() && controller.GetHairTriggerUp();
    }
    public bool GetGripDown() {
        return IsReady() && controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
    }
    public bool GetGrip() {
        return IsReady() && controller.GetPress(SteamVR_Controller.ButtonMask.Grip);
    }
    public bool GetGripUp() {
        return IsReady() && controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
    }
    public bool GetTouchPadDown() {
        return IsReady() && controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool GetTouchPad() {
        return IsReady() && controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool GetTouchPadUp() {
        return IsReady() && controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool GetTouchPadTouchDown() {
        return IsReady() && controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool GetTouchPadTouch() {
        return IsReady() && controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool GetTouchPadTouchUp() {
        return IsReady() && controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public Vector2 GetTouchPadAxis() {
        return IsReady() ? controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad) : Vector2.zero;
    }
    public bool GetMenuDown() {
        return IsReady() && controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu);
    }
    public bool GetMenu() {
        return IsReady() && controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu);
    }
    public bool GetMenuUp() {
        return IsReady() && controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu);
    }

    // Feedback
    public void TriggerHapticPulse(ushort durationMicroSec = 500, EVRButtonId buttonId = EVRButtonId.k_EButton_SteamVR_Touchpad) {
        controller.TriggerHapticPulse(durationMicroSec, buttonId);
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(InitControllerCoroutine());
    }

    private IEnumerator InitControllerCoroutine() {
        while (true) {
            // Don't need to run this every frame
            yield return new WaitForSeconds(1.0f);

            // We have a controller now, break out of the loop!
            //if (controller != null) { break; }

            // Initialize hand
            //if (handType == HandType.Left || handType == HandType.Right) {
                // Left/right relationship.
                // Wait until we have a clear unique left-right relationship to initialize. 
                //int leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
                //int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
                //if (leftIndex == -1 || rightIndex == -1 || leftIndex == rightIndex) { continue; }

                //int myIndex = (handType == HandType.Right) ? rightIndex : leftIndex;
                if (GetComponent<SteamVR_TrackedObject>().index == SteamVR_TrackedObject.EIndex.None) { continue; }
                int myIndex = (int)GetComponent<SteamVR_TrackedObject>().index;
                //int otherIndex = (handType == HandType.Right) ? leftIndex : rightIndex;

                //if (handType == HandType.Left) myIndex = (int)SteamVR_TrackedObject.EIndex.Device3;
                //if (handType == HandType.Right) myIndex = (int) SteamVR_TrackedObject.EIndex.Device4;
                //if (handType == HandType.Left) Debug.Log("Left: " + GetComponent<SteamVR_TrackedObject>().index);
                //if (handType == HandType.Right) Debug.Log("Right: " + GetComponent<SteamVR_TrackedObject>().index);

                InitController(myIndex);
            //if (otherHand) { otherHand.InitController(otherIndex); }
            /*} else {
                // No left/right relationship. Just wait for a connection
                var vr = SteamVR.instance;
                for (int i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++) {
                    if (vr.hmd.GetTrackedDeviceClass((uint) i) != ETrackedDeviceClass.Controller) { continue; } // Not a controller

                    var device = SteamVR_Controller.Input(i);
                    if (!device.valid) { continue; } // Not valid

                    if ((otherHand != null) && (otherHand.controller != null)) {
                        // Other hand is using this index, so we cannot use it.
                        if (i == (int) otherHand.controller.index) { continue; } // Owned by the other hand
                    }

                    InitController(i);
                }
            }*/
        }
    }

    private void InitController(int index) {
        //if (controller == null) {
            controller = SteamVR_Controller.Input(index);

            //if (handModel) handModel.SetActive(true);
            //controller.TriggerHapticPulse(800);
        //}
    }
}
