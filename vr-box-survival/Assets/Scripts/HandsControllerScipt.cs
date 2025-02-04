using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsControllerScipt : MonoBehaviour
{
    [SerializeField] InputActionReference gripInputAction;
    [SerializeField] InputActionReference triggerInputAction;


    Animator handAnimator;


    private void Awake() {

        handAnimator = GetComponent<Animator>();

    }

    private void OnEnable() {

        gripInputAction.action.performed += gripPressed;
        triggerInputAction.action.performed += triggerPressed;
        handAnimator.SetFloat("Trigger", 1f);
        handAnimator.SetFloat("Grip", 1f);
    }

    private void triggerPressed(InputAction.CallbackContext obj) {
      //  handAnimator.SetFloat("Trigger", obj.ReadValue<float>());

    }

    private void gripPressed(InputAction.CallbackContext obj) {
       // handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }

    private void OnDisable() {
        gripInputAction.action.performed -= gripPressed;
        triggerInputAction.action.performed -= triggerPressed;
    }
}
