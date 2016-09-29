using UnityEngine;
using System.Collections;

public abstract class SteamVRInput: MonoBehaviour
{
    /* 
     * Derive from this class for easy SteamVR input.
     * Override the functions for button pressed (OnTriggerDown, OnGripDown) and touchpad swipes (OnSwipeRight, OnSwipeDown)
     */

    public SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    Vector2 firstTouchPos, secondTouchPos, currentSwipe;
    public float minSwipeLength = 0f;

    void Start()
    {
        if (!trackedObj)
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }
    }

    public virtual void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        // Trigger
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            OnTriggerDown();
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            OnTriggerUp();
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            OnTrigger();
        }

        // Touchpad
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpadDown();
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpadUp();
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpad();
        }

        // Touchpad Swipes 
        ListenForSwipes();

        // Grip
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            OnGripDown();
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            OnGripUp();
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            OnGrip();
        }

        // System
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.System))
        {
            OnSystemDown();
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.System))
        {
            OnSystemUp();
        }
    }

    void ListenForSwipes() // reference: http://forum.unity3d.com/threads/swipe-in-all-directions-touch-and-mouse.165416/
    {
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            firstTouchPos = new Vector2(device.GetAxis().x, device.GetAxis().y);
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            secondTouchPos = new Vector2(device.GetAxis().x, device.GetAxis().y);
            currentSwipe = new Vector2(secondTouchPos.x - firstTouchPos.x, secondTouchPos.y - firstTouchPos.y);

            if (currentSwipe.magnitude < minSwipeLength)
            {
                //return;
            }

            currentSwipe.Normalize();

            // Swipe up
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                OnSwipeUp();
            } // Swipe down
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                OnSwipeDown();

            } // Swipe left
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                OnSwipeLeft();

            } // Swipe right
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                OnSwipeRight();
            }
        }
    }

    public virtual void OnSwipeUp()
    {
        //Debug.Log("swipe up");
    }

    public virtual void OnSwipeDown()
    {
        //Debug.Log("swipe down");
    }

    public virtual void OnSwipeLeft()
    {
        //Debug.Log("swipe left");
    }

    public virtual void OnSwipeRight()
    {
        //Debug.Log("swipe right");
    }

    public virtual void OnTriggerDown()
    {

    }

    public virtual void OnTriggerUp()
    {

    }

    public virtual void OnTrigger()
    {

    }

    public virtual void OnTouchpadDown()
    {

    }

    public virtual void OnTouchpadUp()
    {

    }

    public virtual void OnTouchpad()
    {

    }

    public virtual void OnGripDown()
    {

    }

    public virtual void OnGripUp()
    {

    }

    public virtual void OnGrip()
    {

    }

    public virtual void OnSystemDown()
    {

    }

    public virtual void OnSystemUp()
    {

    }
}
