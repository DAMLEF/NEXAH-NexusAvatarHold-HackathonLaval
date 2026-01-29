using JetBrains.Annotations;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ToolManager : MonoBehaviour
{
    public Transform objectSnapPosition;
    
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
    private GameObject heldItem = null;

    void Start()
    {
        hand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        controller = gameObject.GetComponentInChildren<ControllerAnimator>().gameObject;

        gun = gameObject.GetComponentInChildren<Gun>();
        gun.gameObject.SetActive(false);

        grenadeManager = gameObject.GetComponentInChildren<GrenadeManager>();
        grenadeManager.gameObject.SetActive(false);

        if (!objectSnapPosition) Debug.LogWarning("No object snap position set, objects will no snap to the hand when grabbed");
    }

    private bool bPressedLastFrame = false;
    private bool gripPressedLastFrame = false;
    private Vector3 posLastFrame = Vector3.zero;
    void Update()
    {
        // Get "B" press to switch tool/mode
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
                    heldItem.transform.parent = null;
                    Rigidbody rb = heldItem.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.isKinematic = false;
                        rb.useGravity = true;
                        // Apply velocity
                        rb.AddForce((controller.transform.position - posLastFrame)/Time.deltaTime, ForceMode.VelocityChange);
                    }
                    

                    heldItem = null;
                    // Callback to item script: kinematic/no grav if not in place, snap if it can
                    controller.SetActive(true);
                    currentTool = Tool.CONTROLLER;
                    break;
            }
        }
        bPressedLastFrame = bPressed;
        posLastFrame = controller.transform.position;

    }
    void OnTriggerStay(Collider other)
    {
        // Filter out un-grabbable items
        GameObject item = other.gameObject;
        if (!item.CompareTag("Grabbable")) return;
        // Get "Grip" press to grab object if in CONTROLLER mode
        bool gripPressed = false;
        bool success = hand.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed);
        if (currentTool == Tool.CONTROLLER && success && gripPressed && !gripPressedLastFrame)
        {
            // Make item follow hand movements
            item.transform.SetParent(transform);
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            heldItem = item;

            // Snap to hand center
            if (objectSnapPosition) item.transform.localPosition = objectSnapPosition.localPosition;

            // Change hand state
            controller.SetActive(false);
            currentTool = Tool.OBJECT;
        }
        gripPressedLastFrame = gripPressed;
    }

    
}
