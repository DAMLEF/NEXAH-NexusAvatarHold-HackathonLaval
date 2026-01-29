using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ToolManager : MonoBehaviour
{
    private enum Tool
    {
        CONTROLLER,
        GUN,
        GRENADE,
        OBJECT
    }

    private Tool currentTool = Tool.CONTROLLER;
    private InputDevice hand;
    private GameObject controller;
    private Gun gun;
    private GrenadeManager grenadeManager;

    void Start()
    {
        hand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        controller = gameObject.GetComponentInChildren<ControllerAnimator>().gameObject;

        gun = gameObject.GetComponentInChildren<Gun>();
        gun.gameObject.SetActive(false);

        grenadeManager = gameObject.GetComponentInChildren<GrenadeManager>();
        grenadeManager.gameObject.SetActive(false);
    }

    private bool bPressedLastFrame = false;
    void Update()
    {
        // Get "B" press to switch object
        bool bPressed = false;
        bool success = hand.TryGetFeatureValue(CommonUsages.secondaryButton, out bPressed);
        if (success && bPressed && !bPressedLastFrame)
        {
            switch(currentTool)
            {
                case Tool.CONTROLLER:
                    // Switch to gun
                    controller.SetActive(false);
                    gun.gameObject.SetActive(true);
                    currentTool = Tool.GUN;
                    break;

                case Tool.GUN:
                    // Switch to grenade
                    gun.gameObject.SetActive(false);
                    grenadeManager.gameObject.SetActive(true);
                    currentTool = Tool.GRENADE;
                    break;

                case Tool.GRENADE:
                    // Switch back to controller
                    grenadeManager.gameObject.SetActive(false);
                    controller.SetActive(true);
                    currentTool = Tool.CONTROLLER;
                    break;

                case Tool.OBJECT:
                    // Drop object
                    // Callback to item script: kinematic/no grav if not in place, snap if it can
                    currentTool= Tool.CONTROLLER;
                    break;
            }
        }
        bPressedLastFrame = bPressed;

        // TODO if in CONTROLLER, detect trigger to grab object (either in Update or OnCollisionRemain)
    }
}
