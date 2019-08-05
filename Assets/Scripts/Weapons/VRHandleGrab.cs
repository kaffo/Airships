using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRHandleGrab : MonoBehaviour
{
    private Vector3 initalOffset = new Vector3();

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, startingGrabType, Hand.AttachmentFlags.AllowSidegrade);
            hand.HideGrabHint();
            initalOffset = (hand.transform.position - transform.parent.position).normalized;
        }
    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {

        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject);
            initalOffset = new Vector3();
        } else
        {
            Transform parentTransform = transform.parent;
            Transform handTransform = hand.transform;

            Vector3 reverseForwardVector = Vector3.RotateTowards(initalOffset, (handTransform.position - parentTransform.position).normalized, 2f, 0);

            Quaternion newRotation = Quaternion.LookRotation(reverseForwardVector);
            newRotation *= Quaternion.Euler(0f, 90, 0f);
            newRotation.eulerAngles = new Vector3(0f, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
            parentTransform.rotation = newRotation;
        }
    }
}
