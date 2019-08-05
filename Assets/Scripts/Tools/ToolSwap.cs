using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ToolSwap : MonoBehaviour
{
    public SteamVR_Action_Boolean toolSwapAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ToolSwap");

    public GameObject defaultTool = null;
    public int currentToolIndex = 0;
    public List<GameObject> toolGameObjects = new List<GameObject>();

    private Player player = null;

    private void Start()
    {
        player = Valve.VR.InteractionSystem.Player.instance;
        if (player == null)
        {
            Debug.LogError(this.name + " on " + this.gameObject + " has not been setup correctly!");
            this.enabled = false;
            return;
        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            toolGameObjects.Add(this.transform.GetChild(i).gameObject);

            if (defaultTool != null && defaultTool == toolGameObjects[i])
                toolGameObjects[i].SetActive(true);
            else
                toolGameObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        foreach (Hand hand in player.hands)
        {
            if (hand.noSteamVRFallbackCamera == null && toolSwapAction.GetStateUp(hand.handType))
            {
                GameObject currentTool = toolGameObjects[currentToolIndex];
                currentTool.SetActive(false);

                if (++currentToolIndex >= toolGameObjects.Count)
                {
                    currentToolIndex = 0;
                }

                GameObject newTool = toolGameObjects[currentToolIndex];
                newTool.SetActive(true);
            }
        }
    }
}
