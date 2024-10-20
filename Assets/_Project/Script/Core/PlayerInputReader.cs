using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;


[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "ScriptableObject/Player/InputReader")]
public class PlayerInputReader : SerializedScriptableObject, IPlayerActions
{

    public Subject<Vector2> Move = new Subject<Vector2>();
    private PlayerInputActions _inputActions;

    public Vector3 Direction => _inputActions.Player.Move.ReadValue<Vector2>();
    public void OnEnable()
    {
        if (_inputActions != null)
            return;

        Initialize();
    }
    private void Initialize()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.SetCallbacks(this);
        Move = new Subject<Vector2>();

    }

    public void EnablePlayerActions()
    {
        _inputActions.Enable();
    

        //_inputActions.Player.Enable();
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnIdleDance(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractNormal(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.OnNext(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

 
}
