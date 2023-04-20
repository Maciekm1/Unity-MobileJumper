using UnityEngine;
using System;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    //  Singleton
    public static InputManager Instance { get; set; }

    //  References
    private PlayerInputActions playerInputActions;
    private Grapple grapple;

    //  Events

    public event Action OnScreenSwipe;

    public event Action OnScreenHold;
    public event Action OnScreenRelease;

    // Booleans

    private bool hasJumped = false;

    private void Awake()
    {
        if (Instance != this) { Instance = this; }

        playerInputActions = new PlayerInputActions();

    }

    void Update()
    {

    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        // Screen Swipe
        playerInputActions.Player.Jump.performed += ctx => ScreenSwipe(ctx);

        playerInputActions.Player.TouchingScreen.started += ctx => StartTouchScreen(ctx);
        playerInputActions.Player.TouchingScreen.canceled += ctx => EndTouchScreen(ctx);

        // Screen Swipe END

        // Grapple
        playerInputActions.Player.Grapple.started += ctx => StartGrapple(ctx);
        playerInputActions.Player.Grapple.performed += ctx => GrapplePerformed(ctx);
        playerInputActions.Player.Grapple.canceled += ctx => EndGrapple(ctx);

        // Grapple END
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= ctx => ScreenSwipe(ctx);

        // Screen Swipe
        playerInputActions.Player.TouchingScreen.started -= ctx => StartTouchScreen(ctx);
        playerInputActions.Player.TouchingScreen.canceled -= ctx => EndTouchScreen(ctx);

        // Screen Swipe END

        // Grapple
        playerInputActions.Player.Grapple.started -= ctx => StartGrapple(ctx);
        playerInputActions.Player.Grapple.performed -= ctx => GrapplePerformed(ctx);
        playerInputActions.Player.Grapple.canceled -= ctx => EndGrapple(ctx);
        // Grapple END

        playerInputActions.Disable();
    }

    // Screen Swipe
    private void ScreenSwipe(InputAction.CallbackContext context)
    {
        if (!hasJumped && context.ReadValue<float>() > 0)
        {
            if (OnScreenSwipe != null) OnScreenSwipe.Invoke();
            hasJumped = true;
        }
    }

    private void StartTouchScreen(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.GameStarted)
        {
            GameManager.Instance.GameStarted = true;
            GameManager.Instance.OnGameStart.Invoke();
            return;
        }
    }

    private void EndTouchScreen(InputAction.CallbackContext context)
    {
        if (hasJumped)
        {
            hasJumped = false;
        }
    }

    // Screen Swipe END


    // Grapple

    private void StartGrapple(InputAction.CallbackContext context)
    {
        if (!hasJumped)
        {
            Debug.Log("start Hold");
        }
    }

    private void GrapplePerformed(InputAction.CallbackContext context)
    {
        if (!hasJumped)
        {
            if (OnScreenHold != null) OnScreenHold.Invoke();
            Debug.Log("Start Grapple");
        }

    }

    private void EndGrapple(InputAction.CallbackContext context)
    {
        grapple = FindObjectOfType<Grapple>();
        if (grapple != null)
        {
            if (grapple.StartedGrapple)
            {
                if (OnScreenRelease != null) OnScreenRelease.Invoke();
                Debug.Log("Grapple Finished");
            }
        }

    }

    // Grapple END

}
