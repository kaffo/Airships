using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PromptOnActivate : MonoBehaviour
{
    public SteamVR_Action_Boolean actionToPrompt = null;
    public string textToPrompt = "";
    public float timeToPrompt = 2f;


    private Player player = null;

    private void Start()
    {
        player = Valve.VR.InteractionSystem.Player.instance;
        if (player == null || actionToPrompt == null)
        {
            Debug.LogError(this.name + " on " + this.gameObject + " has not been setup correctly!");
            this.enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        foreach (Hand hand in player.hands)
        {
            if (hand.noSteamVRFallbackCamera == null)
            {
                ControllerButtonHints.ShowTextHint(hand, actionToPrompt, textToPrompt);
                Invoke("DisableHint", timeToPrompt);
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke("DisableHint");
        DisableHint();
    }

    private void DisableHint()
    {
        foreach (Hand hand in player.hands)
        {
            if (hand.noSteamVRFallbackCamera == null)
            {
                ControllerButtonHints.HideTextHint(hand, actionToPrompt);
            }
        }
    }
}
