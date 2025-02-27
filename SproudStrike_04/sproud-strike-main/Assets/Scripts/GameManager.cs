using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static InputManager InstanceInputManager => instance != null ? instance : new ();

    private static InputManager instance;
    
    private InputManager inputManager;

    private void Awake()
    {
        instance = new();
        inputManager = new();
    }

    private void OnDestroy()
    {
        inputManager.Destroy();
    }
}
