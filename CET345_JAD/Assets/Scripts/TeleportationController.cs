using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;
    private InputAction thumbStick;
    private bool active;

    void Start()
    {
        //disables ray so no activae when not in use
        //Creates and sets up the teleport input action maps for use
        InputAction activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += TelportActivated;

        InputAction cancel = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += TeleportCancelled;

        thumbStick = actionAsset.FindActionMap("XRI RightHand").FindAction("Move");
        thumbStick.Enable();
    }


    void Update()
    {
        if (!active)
        {
            return;
        }

        if (thumbStick.triggered)
        {
            return;
        }

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
            active = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
        provider.QueueTeleportRequest(request);
    }

    //function to be called when teleport sequence is activated
    private void TelportActivated(InputAction.CallbackContext context)
    {
        rayInteractor.lineType = XRRayInteractor.LineType.ProjectileCurve;
        provider.enabled = true;
        active = true;
    }

    //function called when teleport action is cancelled
    private void TeleportCancelled(InputAction.CallbackContext context)
    {
        rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
        active = false;
        provider.enabled = false;
    }
}