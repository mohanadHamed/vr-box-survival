using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class UI_InteractionController : MonoBehaviour
{
    [SerializeField]
    GameObject UIController;

    [SerializeField]
    GameObject BaseController;

    [SerializeField]
    InputActionReference inputActionReference_UISwitcher;

    bool isUICanvasActive = false;

    [SerializeField]
    GameObject UICanvasGameobject;

    [SerializeField]
    Vector3 positionOffsetForUICanvasGameobject;

    [SerializeField]
    Animator leftHandAnimator;


    private void OnEnable()
    {
        inputActionReference_UISwitcher.action.performed += ActivateUIMode;
    }
    private void OnDisable()
    {
        inputActionReference_UISwitcher.action.performed -= ActivateUIMode;

    }

    private void Start()
    {
        //Deactivating UI Canvas Gameobject by default
        UICanvasGameobject.SetActive(false);

        isUICanvasActive = false;
        //Deactivating UI Controller by default
        UIController.GetComponent<XRRayInteractor>().enabled = false;
        UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

        ActivateUIMode(new InputAction.CallbackContext());
    }

    public void SetUiVisibility(bool nextVisibleState) {
        isUICanvasActive = !nextVisibleState;

        ActivateUIMode(new InputAction.CallbackContext());
    }

    /// <summary>
    /// This method is called when the player presses UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    /// <param name="obj"></param>
    private void ActivateUIMode(InputAction.CallbackContext obj)
    {
        Debug.Log("ActivateUIMode is called");

        if (!isUICanvasActive)
        {
            isUICanvasActive = true;

            //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = true;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = true;

            //Deactivating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = false;

            //Adjusting the transform of the UI Canvas Gameobject according to the VR Player transform
            Vector3 positionVec = new Vector3(UIController.transform.position.x, positionOffsetForUICanvasGameobject.y, UIController.transform.position.z);
            Vector3 directionVec = (GameManager.Instance.TrainingWallPos - GameManager.Instance.LocalPlayerGoalWallPos).normalized;
            directionVec.y = 0f;
            UICanvasGameobject.transform.position = positionVec + positionOffsetForUICanvasGameobject.magnitude * directionVec;
            UICanvasGameobject.transform.rotation = Quaternion.LookRotation(directionVec);

            //Activating the UI Canvas Gameobject
            UICanvasGameobject.SetActive(true);
            leftHandAnimator.SetFloat("Grip", 0);
            leftHandAnimator.SetFloat("Trigger", 0.5f);

            Time.timeScale = 0;

        }
        else
        {

            Time.timeScale = 1;

            isUICanvasActive = false;

            //De-Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            UIController.GetComponent<XRRayInteractor>().enabled = false;
            UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

            //Activating Base Controller by disabling its XR Direct Interactor
            BaseController.GetComponent<XRDirectInteractor>().enabled = true;

            //De-Activating the UI Canvas Gameobject
            UICanvasGameobject.SetActive(false);
            leftHandAnimator.SetFloat("Grip", 1f);
            leftHandAnimator.SetFloat("Trigger", 1f);
        }

    }
}
