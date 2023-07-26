using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MobileCameraTanslate : MonoBehaviour
{
    [SerializeField] GameObject joystick;
    [SerializeField] private TextMeshProUGUI touchpos1text, touchpos2text;

    [SerializeField] private PlayerInput playerInput;
    private InputAction primaryFingerTouch, secondaryFingerTouch;

    private Vector2 touchPos1, touchPos2;

    private void Awake()
    {
        primaryFingerTouch = playerInput.actions["PrimaryFingerTouch"];
        secondaryFingerTouch = playerInput.actions["SecondaryFingerTouch"];
    }

    private void OnEnable()
    {
        primaryFingerTouch.performed += PrimaryFingerTouch;
        secondaryFingerTouch.performed += SecondaryFingerTouch;
    }

    void PrimaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos1 = context.ReadValue<Vector2>();
        touchpos1text.text = touchPos1.ToString();
    }

    void SecondaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos2 = context.ReadValue<Vector2>();
        touchpos2text.text = touchPos2.ToString();
    }


    //if finger 1 is touching the left side of the screen enable the joystick
}
