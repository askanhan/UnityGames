using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public event Action <Vector2> OnMove = delegate { };
    public event Action OnShoot = delegate { };

    private InputSetup inputSetup;

    public InputManager()
    {
        Setup();
    }

    public void Setup()
    {
        inputSetup = new();
        inputSetup.Enable();
        
        inputSetup.Land.Move.performed += (ctx) => Move(ctx);
        inputSetup.Land.Shoot.performed += (_) => Shoot();
        
        inputSetup.Land.Move.canceled += (_) => StopMove();
    }

    public void Destroy()
    {
        inputSetup.Disable();
    }
    
    private void Move(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }
    
    private void StopMove()
    {
        OnMove?.Invoke(Vector2.zero);
    }

    private void Shoot()
    {
        OnShoot?.Invoke();
    }
}
