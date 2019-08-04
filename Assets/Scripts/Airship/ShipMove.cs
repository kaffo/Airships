using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ShipMove : MonoBehaviour
{
    //public SteamVR_Action_Vector2 airshipMoveDirection = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("AirshipMoveDirection");
    public SteamVR_Action_Boolean airshipMoveAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("AirshipMoveAction");

    public float maxSpeed = 1f;

    private Vector2 toMove = new Vector2();
    private Player player = null;

    private void Start()
    {
        player = Valve.VR.InteractionSystem.Player.instance;
        if (player == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> ShipMove: No Player instance found in map.");
            this.enabled = false;
            return;
        }
    }

    void Update()
    {
        bool triggerPressed = false;
        foreach (Hand hand in player.hands)
        {
            if (hand.noSteamVRFallbackCamera == null && airshipMoveAction.GetState(hand.handType))
            {
                triggerPressed = true;
                //Debug.Log("Hand fwd:" + hand.transform.forward);
                toMove = new Vector2(hand.transform.forward.x, hand.transform.forward.z);
                //Debug.Log("toMove:" + toMove);
            }
        }
        if (!triggerPressed && toMove.magnitude != 0)
        {
            toMove = new Vector2();
        }
    }

    private void FixedUpdate()
    {
        if (toMove.magnitude > 0)
        {
            Vector2 currentPos = new Vector2(this.transform.position.x, this.transform.position.z);
            Vector2 desiredPos = currentPos + toMove;
            //Debug.Log("desiredPos:" + desiredPos);
            Vector2 endPos = Vector2.MoveTowards(currentPos, desiredPos, maxSpeed);
            this.transform.position = new Vector3(endPos.x, this.transform.position.y, endPos.y);
        }
    }
}
